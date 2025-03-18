using System.Runtime.CompilerServices;

namespace BlazorBuilds.Components.Common.Extensions;

public static class UtilityExtensions
{

    public static void WhenTrue(this bool boolValue, Action act_whenTrue)
    {
        if (boolValue == true) act_whenTrue();
    }

    public static void WhenFalse(this bool boolValue, Action act_whenFalse)
    {
        if (boolValue == false) act_whenFalse();
    }
    public static void WhenTrueElse(this bool boolValue, Action act_whenTrue, Action act_whenFalse)
    {
        if (boolValue == true) act_whenTrue(); else act_whenFalse();
    }

    public static async Task WhenTrue(this bool boolValue, Func<Task> do_whenTrue)
    {
        if (boolValue == true) await do_whenTrue();
    }

    public static async ValueTask WhenTrue(this bool boolValue, Func<ValueTask> do_whenTrue)
    {
        if (boolValue == true) await do_whenTrue();
    }
    public static async ValueTask WhenTrueElse(this bool boolValue, Func<ValueTask> do_whenTrue, Func<ValueTask> do_whenFalse)
    {
        if (boolValue == true) await do_whenTrue(); else await do_whenFalse();
    }
    public static async Task WhenTrueElse(this bool boolValue, Func<Task> do_whenTrue, Func<Task> do_whenFalse)
    {
        if (boolValue == true) await do_whenTrue(); else await do_whenFalse();
    }

     
}