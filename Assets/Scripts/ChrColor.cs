using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChrColor {
    public static ColorAttr Red = new ColorAttr(
        new Color32(255, 0, 0, 255),
        ColorName.Red,
        ColorName.Green,
        new List<ColorName> { ColorName.RedOrange, ColorName.Magenta },
        new List<ColorName> { ColorName.Blue, ColorName.Yellow }
    );

    public static ColorAttr RedOrange = new ColorAttr(
        new Color32(236, 90, 41, 255),
        ColorName.RedOrange,
        ColorName.Turquoise,
        new List<ColorName> { ColorName.Red, ColorName.Orange },
        new List<ColorName> { ColorName.Violet, ColorName.Lime }
    );

    public static ColorAttr Orange = new ColorAttr(
        new Color32(255, 165, 0, 255),
        ColorName.Orange,
        ColorName.Turquoise,
        new List<ColorName> { ColorName.RedOrange, ColorName.OrangeYellow },
        new List<ColorName> { ColorName.Purple, ColorName.Green }
    );

    public static ColorAttr OrangeYellow = new ColorAttr(
        new Color32(247, 176, 62, 255),
        ColorName.OrangeYellow,
        ColorName.Violet,
        new List<ColorName> { ColorName.Orange, ColorName.Yellow },
        new List<ColorName> { ColorName.Magenta, ColorName.Turquoise }
    );

    public static ColorAttr Yellow = new ColorAttr(
        new Color32(255, 255, 0, 255),
        ColorName.Yellow,
        ColorName.Purple,
        new List<ColorName> { ColorName.OrangeYellow, ColorName.Lime },
        new List<ColorName> { ColorName.Red, ColorName.Blue }
    );

    public static ColorAttr Lime = new ColorAttr(
        new Color32(139, 196, 59, 255),
        ColorName.Lime,
        ColorName.Magenta,
        new List<ColorName> { ColorName.Yellow, ColorName.Green },
        new List<ColorName> { ColorName.RedOrange, ColorName.Violet }
    );

    public static ColorAttr Green = new ColorAttr(
        new Color32(0, 255, 0, 255),
        ColorName.Green,
        ColorName.Red,
        new List<ColorName> { ColorName.Turquoise, ColorName.Lime },
        new List<ColorName> { ColorName.Orange, ColorName.Purple }
    );

    public static ColorAttr Turquoise = new ColorAttr(
        new Color32(64, 224, 208, 255),
        ColorName.Turquoise,
        ColorName.Orange,
        new List<ColorName> { ColorName.Blue, ColorName.Violet },
        new List<ColorName> { ColorName.OrangeYellow, ColorName.Magenta }
    );

    public static ColorAttr Blue = new ColorAttr(
        new Color32(0, 0, 255, 255),
        ColorName.Blue,
        ColorName.Orange,
        new List<ColorName> { ColorName.Violet, ColorName.Turquoise },
        new List<ColorName> { ColorName.Yellow, ColorName.Red }
    );

    public static ColorAttr Violet = new ColorAttr(
        new Color32(138, 43, 226, 255),
        ColorName.Violet,
        ColorName.OrangeYellow,
        new List<ColorName> { ColorName.Purple, ColorName.Blue },
        new List<ColorName> { ColorName.Lime, ColorName.RedOrange }
    );

    public static ColorAttr Purple = new ColorAttr(
        new Color32(128, 0, 128, 255),
        ColorName.Purple,
        ColorName.Yellow,
        new List<ColorName> { ColorName.Magenta, ColorName.Violet },
        new List<ColorName> { ColorName.Green, ColorName.Orange }
    );

    public static ColorAttr Magenta = new ColorAttr(
        new Color32(199, 21, 133, 255),
        ColorName.Magenta,
        ColorName.Lime,
        new List<ColorName> { ColorName.Red, ColorName.Purple },
        new List<ColorName> { ColorName.Turquoise, ColorName.OrangeYellow }
    );

    public static ColorAttr White = new ColorAttr(
        new Color32(255, 255, 255, 255),
        ColorName.White,
        ColorName.All,
        new List<ColorName> { ColorName.All },
        new List<ColorName> { ColorName.All }
    );

    public static ColorAttr Black = new ColorAttr(
        new Color32(0, 0, 0, 255),
        ColorName.Black,
        ColorName.None,
        new List<ColorName> { ColorName.None },
        new List<ColorName> { ColorName.None }
    );

    public static List<ColorAttr> colors = new List<ColorAttr> {
        Red, RedOrange, Orange, OrangeYellow, Yellow, Lime, Green,
        Turquoise, Blue, Violet, Purple, Magenta, White, Black
    };

    public static Harmony DefineHarmony(ColorAttr currentColor, ColorAttr collidingWithColor) {
        if (collidingWithColor.chrColorName == ColorName.White) {
            return Harmony.All;
        }

        else if (collidingWithColor.chrColorName == ColorName.Black) {
            return Harmony.None;
        }
        
        else if (currentColor == collidingWithColor) {
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
    public Color32 rgbValue { get; }
    public ColorName chrColorName { get; }
    public ColorName complementaryColor { get; }
    public List<ColorName> analogueColors { get; }
    public List<ColorName> triadicColors { get; }

    public ColorAttr(
        Color32 InRGBValue, 
        ColorName InChrColorName, 
        ColorName InComplementaryColor, 
        List<ColorName> InAnalogueColors, 
        List<ColorName> InTriadicColors
    ) {
        rgbValue = InRGBValue;
        chrColorName = InChrColorName;
        complementaryColor = InComplementaryColor;
        analogueColors = InAnalogueColors;
        triadicColors = InTriadicColors;
    }
}