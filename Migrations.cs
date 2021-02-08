﻿using Etch.OrchardCore.Fields.Colour.Fields;
using Etch.OrchardCore.Fields.Colour.Settings;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace Etch.OrchardCore.Widgets
{
    public class Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            #region Html Attributes

            _contentDefinitionManager.AlterPartDefinition("HtmlAttributesPart", part => part
                .Attachable()
                .WithDescription("Customise common attributes on HTML element.")
                .WithDisplayName("HTML Attributes"));

            #endregion HtmlAttributes

            return 1;
        }

        public int UpdateFrom1()
        {
            _contentDefinitionManager.AlterPartDefinition("BackgroundPart", part => part
                .Attachable()
                .WithDescription("Define background properties.")
                .WithDisplayName("Background")
                .WithField("BackgroundColour", field => field
                    .OfType(nameof(ColourField))
                    .WithDisplayName("Background Colour")
                    .WithPosition("0")
                    .WithSettings(new ColourFieldSettings
                    {
                        AllowCustom = true,
                        AllowTransparent = true,
                        DefaultValue = "transparent",
                        UseGlobalColours = true
                    })
                )
                .WithField("BackgroundPattern", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Background Pattern")
                    .WithPosition("1")
                    .WithEditor("PredefinedList")
                    .WithSettings(new TextFieldPredefinedListEditorSettings
                    {
                        Editor = EditorOption.Dropdown,
                        Options = new ListValueOption[] { new ListValueOption { Name = "None", Value = string.Empty } }
                    })
                )
                .WithField("InvertTextColour", field => field
                    .OfType(nameof(BooleanField))
                    .WithDisplayName("Invert Text Colour")
                    .WithPosition("2")
                    .WithSettings(new BooleanFieldSettings
                    {
                        Hint = "Change text colour to light, recommended when using a dark background.",
                        Label = "Invert Text Colour"
                    })
                )
                .WithField("BackgroundFixed", field => field
                    .OfType(nameof(BooleanField))
                    .WithDisplayName("Background Fixed")
                    .WithPosition("3")
                    .WithSettings(new BooleanFieldSettings
                    {
                        Hint = "Fix background in place to give a faux parallax effect.",
                        Label = "Fix Background Pattern"
                    })
                ));

            return 2;
        }
    }
}