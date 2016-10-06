using System.Collections.Generic;

namespace NitrohGG.Edwin
{
    public class HearthstoneAssets
    {
        private const string HearthstoneDataDirectory = "Data";

        private static readonly List<string> FileNames = new List<string>
        {
            "actors0.unity3d",
            "boards0.unity3d",
            "cardbacks0.unity3d",
            "cards0.unity3d",
            "cardtextures0.unity3d",
            "cardxml0.unity3d",
            "fonts0.unity3d",
            "gameobjects0.unity3d",
            "movies0.unity3d",
            "premiummaterials0.unity3d",
            "shared.unity3d",
            "soundprefabs0.unity3d",
            "sounds0.unity3d",
            "spells0.unity3d",
            "textures0.unity3d",
            "uiscreens0.unity3d"
        };

        public List<string> GetFileNames() => FileNames;
    }
}
