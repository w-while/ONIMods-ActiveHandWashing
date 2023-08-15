
using Database;
using HarmonyLib;
using JetBrains.Annotations;
using PeterHan.PLib.PatchManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace ActiveHandWashing
{
    public class Patches
    {
        [PLibMethod(RunAt.BeforeDbInit)]
        internal static void BeforeDbInit()
        {
            SanchozzONIMods.Lib.Utils.InitLocalization(typeof(ActiveHandWashingStrings));
        }
        [HarmonyPatch(typeof(HandSanitizer.States) , nameof(HandSanitizer.States.InitializeStates))]
        public static class HandSanitizer_States_InitializeStates_Patch
        {
            public static void Postfix(HandSanitizer.States __instance)
            {
                __instance.ready.ToggleChore(CreateHandwashChore , __instance.notready);
            }
            public static readonly Chore.Precondition DoesDupeNeedWashHand = new Chore.Precondition
            {
                id = "DoesDupeNeedWashHand" ,
                description = ActiveHandWashingStrings.DUPLICANTS.CHORES.PRECONDITIONS.DOES_DUPE_NEED_WASH_HANDS ,
                fn = delegate (ref Chore.Precondition.Context context , object data)
                {
                    if ((context.consumerState.worker.TryGetComponent(out PrimaryElement
                element) && element.DiseaseIdx != Klei.SimUtil.DiseaseInfo.Invalid.idx && element.DiseaseCount > 0))
                    {
                        return true;
                    }
                    return false;
                }
            };
            public static readonly Chore.Precondition DoesDupeAtWashTime = new Chore.Precondition
            {
                id = "DoesDupeAtWashTime" ,
                description = ActiveHandWashingStrings.DUPLICANTS.CHORES.PRECONDITIONS.DOES_DUPE_AT_WASH_TIME ,
                fn = delegate (ref Chore.Precondition.Context context , object data)
                {
                    if (Options.Instance.RecreationTime || context.consumerState.scheduleBlock?.GroupId == "Recreation")
                    {
                        return true;
                    }
                    if (Options.Instance.HygieneTime || context.consumerState.scheduleBlock?.GroupId == "Hygiene")
                    {
                        return true;
                    }
                    if (Options.Instance.SleepTime || context.consumerState.scheduleBlock?.GroupId == "Sleep")
                    {
                        return true;
                    }
                    if (Options.Instance.WorkTime || context.consumerState.scheduleBlock?.GroupId == "Worktime")
                    {
                        return true;
                    }

                    return false;
                }
            };
            public static ChoreType activeHandWashingType = null;
            private static Chore CreateHandwashChore(HandSanitizer.SMInstance instance)
            {
                if (activeHandWashingType == null)
                {
                    activeHandWashingType = (ChoreType)typeof(ChoreTypes).GetMethod("Add" , BindingFlags.Instance | BindingFlags.NonPublic).
                        Invoke(Db.Get().ChoreTypes , new object[] {
                                "ActiveHandWashing", new string[0], "", new string[0],
                                ActiveHandWashing.ActiveHandWashingStrings.DUPLICANTS.CHORES.ACTIVEHANDWASHING.NAME.ToString(),
                                ActiveHandWashing.ActiveHandWashingStrings.DUPLICANTS.CHORES.ACTIVEHANDWASHING.STATUS.ToString(),
                                ActiveHandWashing.ActiveHandWashingStrings.DUPLICANTS.CHORES.ACTIVEHANDWASHING.TOOLTIP.ToString(),
                                false,
                                -1,
                                null
                        });
                }
                Chore c = new WorkChore<HandSanitizer.Work>(activeHandWashingType , instance.master , null , run_until_complete: false ,
                    null , null , null , allow_in_red_alert: false , null ,
                    ignore_schedule_block: true , only_when_operational: true , null , is_preemptable: false , allow_in_context_menu: true ,
                    allow_prioritization: true , Options.Instance.priorityClass , Options.Instance.prioritValue);
                c.AddPrecondition(DoesDupeNeedWashHand);
                c.AddPrecondition(DoesDupeAtWashTime);

                return c;
            }
        }
    }
}
