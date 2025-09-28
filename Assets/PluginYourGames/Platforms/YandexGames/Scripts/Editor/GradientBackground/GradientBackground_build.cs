#if YandexGamesPlatform_yg
namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuild
    {
        public static void GradientBackground()
        {
            var pathCSS =
                $"{InfoYG.CORE_FOLDER_YG2}/Platforms/YandexGames/Scripts/Editor/GradientBackground/GradientBackground.css";

            if (infoYG.Templates.backgroundImgFormat == InfoYG.TemplatesSettings.BackgroundImageFormat.Gradient)
            {
                var textCopy = ManualFileTextCopy(pathCSS);

                textCopy = textCopy.Replace("___CLASS___", "#unity-canvas");

                textCopy = textCopy.Replace("color1",
                    ConvertToRGBA(infoYG.Templates.gradientBackgroundByLoadGame.color1));
                textCopy = textCopy.Replace("color2",
                    ConvertToRGBA(infoYG.Templates.gradientBackgroundByLoadGame.color2));

                if (!infoYG.Templates.gradientBackgroundByLoadGame.radial)
                {
                    textCopy = textCopy.Replace("radial-gradient", "linear-gradient");
                    textCopy = textCopy.Replace("circle",
                        $"{infoYG.Templates.gradientBackgroundByLoadGame.angleInclination}deg");
                }

                styleFile += $"\n\n\n{textCopy}";
            }

            if (infoYG.Templates.fixedAspectRatio && infoYG.Templates.fillBackground)
            {
                if (infoYG.Templates.imageBackground)
                {
                    var imageName = infoYG.Templates.imageName;
                    var line =
                        $"document.body.style.background = \"url('Images/{imageName}') center / cover no-repeat\";";
                    indexFile = indexFile.Replace("// Fill Background [Build Modify]", line);
                }
                else
                {
                    var textCopy = ManualFileTextCopy(pathCSS);

                    textCopy = textCopy.Replace("___CLASS___", "body");

                    textCopy = textCopy.Replace("color1",
                        ConvertToRGBA(infoYG.Templates.gradientBackgroundByAspectRatio.color1));
                    textCopy = textCopy.Replace("color2",
                        ConvertToRGBA(infoYG.Templates.gradientBackgroundByAspectRatio.color2));

                    if (!infoYG.Templates.gradientBackgroundByAspectRatio.radial)
                    {
                        textCopy = textCopy.Replace("radial-gradient", "linear-gradient");
                        textCopy = textCopy.Replace("circle",
                            $"{infoYG.Templates.gradientBackgroundByAspectRatio.angleInclination}deg");
                    }

                    styleFile += $"\n\n\n{textCopy}";
                }
            }
        }
    }
}
#endif