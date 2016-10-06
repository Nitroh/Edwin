using System.Collections.Generic;

namespace NitrohGG.Edwin
{
    public static class CardDefinitions
    {

        //TODO: store this data better
        private const string HearthstoneDataDirectory = "Data";

        private static readonly List<string> AllFileNames = new List<string>
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

        private static readonly List<string> CardDefinitionFileNames = new List<string> { "cardxml0.unity3d" };

        public static List<string> GetCardDefinitons()
        {
            //TODO: steps for getting card definitons
            //1) load file and parse basic data
            //2) load assets (grab index = 0)
            //3) parse asset for <CardDefs> and grab that data
            return null;
        }
    }
}
