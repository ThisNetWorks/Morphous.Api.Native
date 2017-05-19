using Morphous.Api;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Morphous.Api.Native.Shapes
{
    public class MediaShapes : ApiShapesBase, IShapeTableProvider
    {
        public MediaShapes()
        {
        }
        
        public void Discover(ShapeTableBuilder builder)
        {
        }

        [Shape(bindingType: "Translate")]
        public void Media__api__Forms(dynamic Display, dynamic Shape, TextWriter Output)
        {
            using (Display.ViewDataContainer.Model.Node("a-list-item"))
            {
                Display.ViewDataContainer.Model.Id = Shape.ContentItem.Id;
                Display.ViewDataContainer.Model.ContentType = Shape.ContentItem.ContentType;
                Display.ViewDataContainer.Model.DisplayType = Shape.Metadata.DisplayType;

                foreach (var value in Shape.Properties.Values)
                {
                    if (value is IShape)
                    {
                        var shape = (dynamic)value;

                        if (shape.Metadata.Type == "ContentZone" && shape.ZoneName != "Child")
                        {
                            Display(shape);
                        }
                    }
                }
            }
        }

        [Shape(bindingType: "Translate")]
        public void Parts_Image__api__Forms(dynamic Display, dynamic Shape, TextWriter Output)
        {
            using (Display.ViewDataContainer.Model.Node("a-list-item"))
            {
                Display.ViewDataContainer.Model.Type = Shape.ContentPart.PartDefinition.Name;

                var mediaPart = Shape.ContentPart.ContentItem.MediaPart;
                Display.ViewDataContainer.Model.Url = mediaPart.MediaUrl;
                Display.ViewDataContainer.Model.AlternateText = mediaPart.AlternateText;
                Display.ViewDataContainer.Model.Width = Shape.ContentPart.Width;
                Display.ViewDataContainer.Model.Height = Shape.ContentPart.Height;
            }
        }

        [Shape(bindingType: "Translate")]
        public void Parts_Image_Summary__api__Forms(dynamic Display, dynamic Shape, TextWriter Output)
        {
            Parts_Image__api__Forms(Display, Shape, Output);
        }

    }
}
