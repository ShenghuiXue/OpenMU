﻿// <copyright file="ObservableExtensions.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace MUnique.OpenMU.GameLogic;

using MUnique.OpenMU.GameLogic.Views;

/// <summary>
/// Extensions for <see cref="IObservable"/> objects.
/// </summary>
public static class ObservableExtensions
{
    /// <summary>
    /// Executes the action for all observers of the observable.
    /// </summary>
    /// <typeparam name="TViewPlugIn">The type of the <see cref="IViewPlugIn"/>.</typeparam>
    /// <param name="observable">The observable.</param>
    /// <param name="action">The action.</param>
    /// <param name="includeThis">if set to <c>true</c> the <paramref name="action" /> should be done for <paramref name="observable" /> too.</param>
    public static ValueTask ForEachWorldObserverAsync<TViewPlugIn>(this IObservable observable, Func<TViewPlugIn, ValueTask> action, bool includeThis)
        where TViewPlugIn : class, IViewPlugIn
    {
        return observable.ForEachObservingAsync<IWorldObserver>(o => o.InvokeViewPlugInAsync(action), includeThis);
    }

    /// <summary>
    /// Invokes the view plug in asynchronously and catches possible errors.
    /// </summary>
    /// <typeparam name="TViewPlugIn">The type of the view plug in.</typeparam>
    /// <param name="observer">The observer.</param>
    /// <param name="action">The action.</param>
    public static async ValueTask InvokeViewPlugInAsync<TViewPlugIn>(this IWorldObserver observer, Func<TViewPlugIn, ValueTask> action)
        where TViewPlugIn : class, IViewPlugIn
    {
        if (observer.ViewPlugIns.GetPlugIn<TViewPlugIn>() is { } plugIn)
        {
            try
            {
                await action(plugIn).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                observer.Logger.LogError(ex, $"Error when invoking view action of {typeof(TViewPlugIn)}");
            }
        }
    }

    /// <summary>
    /// Gets the observing player with the specified identifier, if found. Otherwise, null.
    /// </summary>
    /// <param name="observable">The observable.</param>
    /// <param name="playerId">The player identifier.</param>
    /// <returns>The observing player with the specified identifier, if found. Otherwise, null.</returns>
    public static async ValueTask<Player?> GetObservingPlayerWithIdAsync(this IObservable observable, ushort playerId)
    {
        using var readerLock = await observable.ObserverLock.ReaderLockAsync();
        return observable.Observers.OfType<Player>().FirstOrDefault(p => p.Id == playerId);
    }

    /// <summary>
    /// Executes the action for all observing players of the observable.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="IWorldObserver"/>.</typeparam>
    /// <param name="observable">The observable.</param>
    /// <param name="action">The action.</param>
    /// <param name="includeThis">if set to <c>true</c> the <paramref name="action" /> should be done for <paramref name="observable" /> too.</param>
    /// <returns></returns>
    public static async ValueTask ForEachObservingAsync<T>(this IObservable observable, Func<T, ValueTask> action, bool includeThis)
        where T : class, IWorldObserver
    {
        try
        {
            using var readerLock = await observable.ObserverLock.ReaderLockAsync();
            foreach (var obs in observable.Observers.OfType<T>())
            {
                if (!includeThis && obs.Equals(observable))
                {
                    continue;
                }

                try
                {
                    await action(obs).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    if (obs is ILoggerOwner loggerOwner)
                    {
                        loggerOwner.Logger.LogError(ex, "Error when performing action for {0}", obs);
                    }
                }
            }
        }
        catch (ObjectDisposedException)
        {
        }
    }
}