namespace SorexUI;

internal static class Extensions {
    internal static T IfNull<T>(this T? @this, Func<T> @default) {
        return @this != null ? @this! : @default();
    }
}
