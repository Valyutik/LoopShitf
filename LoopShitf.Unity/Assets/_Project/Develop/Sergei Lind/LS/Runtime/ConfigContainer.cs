// ReSharper disable InconsistentNaming
using Sergei_Lind.LS.Runtime.Utilities;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using System;

namespace Sergei_Lind.LS.Runtime
{
    public sealed class ConfigContainer : ILoadUnit
    {
        public CoreConfig Core;
        
        public UniTask Load()
        {
            var asset = AssetService.R.Load<TextAsset>(RuntimeConstants.Configs.ConfigFileName);
            
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new Vector2Converter());
            JsonConvert.PopulateObject(asset.text, this, settings);
            return UniTask.CompletedTask;
        }
    }
    
    [Serializable]
    public class CoreConfig
    {
        public PlayerConfig Player;
        public EnemyConfig Enemy;
    }

    [Serializable]
    public class PlayerConfig
    {
        public RingConfig Ring;
        public Vector2 Center = Vector2.zero;
        public float CircleSize = 0.5f;
        public float Radius = 1.3f;
        public float StartAngleDeg;
        public float StartSpeed = 180f;
        public float StartDirection = 1f;
        public int Health = 1;
    }

    [Serializable]
    public class RingConfig
    {
        public int SegmentsCount = 64;
        public float Offset = 0.7f;
        public string Color = "#00c4b9";
    }

    [Serializable]
    public class EnemyConfig
    {
        public float SpawnInterval = 2f;
        public float SpawnOffset = 1f;
        public float Speed = 2f;
        public float LifeTime = 10f;
        public int InitialCount = 5;
        public int Damage = 1;
    }
}