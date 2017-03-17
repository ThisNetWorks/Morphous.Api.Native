using Morphous.Api;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Morphous.FormsApi.Shapes
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
                Display.ViewDataContainer.Model.ContentType = Shape.ContentItem.ContentType;
                Display.ViewDataContainer.Model.DisplayType = Shape.Metadata.DisplayType;

                if (Shape.Meta != null)
                {
                    Display(Shape.Meta);
                }

                Display(Shape.Header);
                Display(Shape.Content);

                if (Shape.Footer != null)
                {
                    Display(Shape.Footer);
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
