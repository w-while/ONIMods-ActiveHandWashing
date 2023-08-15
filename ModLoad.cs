using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Database;
using PeterHan.PLib.Options;
using PeterHan.PLib.PatchManager;
using System.Reflection;

namespace ActiveHandWashing
{
    public class ModLoad : KMod.UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new PPatchManager(harmony).RegisterPatchClass(typeof(Patches));
            new POptions().RegisterOptions(this , typeof(Options));
            new PLocalization().Register();


            //MethodInfo info = typeof(HandSanitizer).GetNestedType("WashHandsReactable" , BindingFlags.NonPublic | BindingFlags.Instance).GetMethod("InternalCanBegin");
            //harmony.Patch(info , transpiler: new HarmonyMethod(typeof(Patches) , nameof(Patches.Transpiler)));
            //MethodInfo info = typeof(HandSanitizer).GetNestedType("WashHandsReactable" , BindingFlags.NonPublic | BindingFlags.Instance).GetMethod("InternalCanBegin");
            //harmony.Patch(info , prefix: new HarmonyMethod(typeof(Patches) , nameof(Patches.HandSanitizer_WashHandsReactable_InternalCanBegin_Prefix)));
        }
    }
}