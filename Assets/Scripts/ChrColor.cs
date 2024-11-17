using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChrColor {
    // Estrutura para cores personalizáveis.
    public static ColorAttr Red = new(
        new Color(1f, 0f, 0f, 1f),
        0.372f,
        ColorTemperature.Warm,
        ColorType.Primary,
        ColorName.Red,
        ColorName.Cyan,
        new List<ColorName> { ColorName.Rose, ColorName.Orange },
        new List<ColorName> { ColorName.Green, ColorName.Blue }
    );

    public static ColorAttr Orange = new(
        new Color(1f, 0.5f, 0f, 1f),
        0.414f,
        ColorTemperature.Warm,
        ColorType.Tertiary,
        ColorName.Orange,
        ColorName.Azure,
        new List<ColorName> { ColorName.Red, ColorName.Yellow },
        new List<ColorName> { ColorName.Springgreen, ColorName.Violet }
    );

    public static ColorAttr Yellow = new(
        new Color(1f, 1f, 0f, 1f),
        0.522f,
        ColorTemperature.Warm,
        ColorType.Secondary,
        ColorName.Yellow,
        ColorName.Blue,
        new List<ColorName> { ColorName.Orange, ColorName.Lime },
        new List<ColorName> { ColorName.Cyan, ColorName.Magenta }
    );

    public static ColorAttr Lime = new(
        new Color(0.5f, 1f, 0f, 1f),
        0.63f,
        ColorTemperature.Warm,
        ColorType.Tertiary,
        ColorName.Lime,
        ColorName.Violet,
        new List<ColorName> { ColorName.Orange, ColorName.Green },
        new List<ColorName> { ColorName.Azure, ColorName.Rose }
    );

    public static ColorAttr Green = new(
        new Color(0f, 1f, 0f, 1f),
        0.676f,
        ColorTemperature.Neutral,
        ColorType.Primary,
        ColorName.Green,
        ColorName.Magenta,
        new List<ColorName> { ColorName.Lime, ColorName.Springgreen },
        new List<ColorName> { ColorName.Blue, ColorName.Red }
    );

    public static ColorAttr Springgreen = new(
        new Color(0f, 1f, 0.5f, 1f),
        0.761f,
        ColorTemperature.Cool,
        ColorType.Tertiary,
        ColorName.Springgreen,
        ColorName.Rose,
        new List<ColorName> { ColorName.Green, ColorName.Cyan },
        new List<ColorName> { ColorName.Violet, ColorName.Orange }
    );

    public static ColorAttr Cyan = new(
        new Color(0f, 1f, 1f, 1f),
        0.869f,
        ColorTemperature.Cool,
        ColorType.Secondary,
        ColorName.Cyan,
        ColorName.Red,
        new List<ColorName> { ColorName.Springgreen, ColorName.Azure },
        new List<ColorName> { ColorName.Magenta, ColorName.Yellow }
    );

    public static ColorAttr Azure = new(
        new Color(0f, 0.5f, 1f, 1f),
        0f,
        ColorTemperature.Cool,
        ColorType.Tertiary,
        ColorName.Azure,
        ColorName.Orange,
        new List<ColorName> { ColorName.Cyan, ColorName.Blue },
        new List<ColorName> { ColorName.Rose, ColorName.Lime }
    );

    public static ColorAttr Blue = new(
        new Color(0f, 0f, 1f, 1f),
        0.037f,
        ColorTemperature.Cool,
        ColorType.Primary,
        ColorName.Blue,
        ColorName.Yellow,
        new List<ColorName> { ColorName.Azure, ColorName.Violet },
        new List<ColorName> { ColorName.Red, ColorName.Green }
    );

    public static ColorAttr Violet = new(
        new Color(0.5f, 0f, 1f, 1f),
        0.083f,
        ColorTemperature.Cool,
        ColorType.Tertiary,
        ColorName.Violet,
        ColorName.Lime,
        new List<ColorName> { ColorName.Magenta, ColorName.Rose },
        new List<ColorName> { ColorName.Orange, ColorName.Springgreen }
    );

    public static ColorAttr Magenta = new(
        new Color(1f, 0f, 1f, 1f),
        0.218f,
        ColorTemperature.Neutral,
        ColorType.Secondary,
        ColorName.Magenta,
        ColorName.Green,
        new List<ColorName> { ColorName.Rose, ColorName.Violet },
        new List<ColorName> { ColorName.Yellow, ColorName.Cyan }
    );

    public static ColorAttr Rose = new(
        new Color(1f, 0f, 0.5f, 1f),
        0.338f,
        ColorTemperature.Warm,
        ColorType.Tertiary,
        ColorName.Rose,
        ColorName.Springgreen,
        new List<ColorName> { ColorName.Red, ColorName.Magenta },
        new List<ColorName> { ColorName.Lime, ColorName.Azure }
    );

    public static ColorAttr White = new(
        new Color(1f, 1f, 1f, 1f),
        0f,
        ColorTemperature.Neutral,
        ColorType.All,
        ColorName.White,
        ColorName.All,
        new List<ColorName> { ColorName.All },
        new List<ColorName> { ColorName.All }
    );

    public static ColorAttr Black = new(
        new Color(0f, 0f, 0f, 1f),
        0f,
        ColorTemperature.Neutral,
        ColorType.None,
        ColorName.Black,
        ColorName.None,
        new List<ColorName> { ColorName.None },
        new List<ColorName> { ColorName.None }
    );

    public static List<ColorAttr> colors = new() {
        Red, Orange, Yellow, Lime, Green, Springgreen, Cyan, Azure, Blue, Violet, Magenta, Rose, White, Black
    };

    public static Harmony DetermineHarmony(ColorAttr currentColor, ColorAttr collidingWithColor) {
        if (collidingWithColor == currentColor) {
            return Harmony.Equal;
        }
        
        if (collidingWithColor.chrColorName == ColorName.White) {
            return Harmony.All;
        }

        else if (collidingWithColor.chrColorName == ColorName.Black) {
            return Harmony.None;
        }
        
        else if (currentColor.chrColorName == collidingWithColor.complementaryColor) {
            return Harmony.Complementary;
        }

        else if (collidingWithColor.analogueColors.Contains(currentColor.chrColorName)) {
            return Harmony.Analogue;
        }

        else if (collidingWithColor.triadicColors.Contains(currentColor.chrColorName)) {
            return Harmony.Triadic;
        }

        else { 
            return Harmony.None;
        }
    }
    
    public static ColorAttr FindColorAttr(ColorName searchingColor) {
        foreach (var colorAttr in colors) {
            if (colorAttr.chrColorName == searchingColor) {
                return colorAttr;
            }
        }
    
        Debug.LogError($"ColorAttr {searchingColor} not found.");
        return null;
    }
}

[System.Serializable]
public class ColorAttr {
    public Color rgbValue { get; }
    public float hueShift { get; }
    public ColorTemperature colorTemperature { get; }
    public ColorType colorType { get; }
    public ColorName chrColorName { get; }
    public ColorName complementaryColor { get; }
    public List<ColorName> analogueColors { get; }
    public List<ColorName> triadicColors { get; }

    public ColorAttr(
        Color rgbValue,
        float hueShift,
        ColorTemperature colorTemperature,
        ColorType colorType,
        ColorName chrColorName,
        ColorName complementaryColor,
        List<ColorName> analogueColors,
        List<ColorName> triadicColors
    ) {
        this.rgbValue = rgbValue;
        this.hueShift = hueShift;
        this.colorTemperature = colorTemperature;
        this.colorType = colorType;
        this.chrColorName = chrColorName;
        this.complementaryColor = complementaryColor;
        this.analogueColors = analogueColors;
        this.triadicColors = triadicColors;
    }
}