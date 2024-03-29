﻿namespace IvanLopes.Forecast.Application.InfraServices
{
    public interface ICacheService
    {
        T? Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? expiry = null);
        void Remove(string key);
    }

}
