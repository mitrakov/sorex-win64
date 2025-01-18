namespace SorexUI;

public static class Extensions {
    public static T IfNull<T>(this T? @this, Func<T> @default) {
        return @this != null ? @this! : @default();
    }
}
