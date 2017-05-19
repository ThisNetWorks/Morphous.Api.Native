using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Morphous.Api;
using Morphous.Api.Extensions;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using System.Collections.Specialized;

namespace Morphous.Api.Native.Shapes {
    public class CoreShapes : ApiShapesBase, IShapeTableProvider {

        public void Discover(ShapeTableBuilder builder) {

        }

        [Shape(bindingType: "Translate")]
        public void Content__api__Forms(dynamic Display, dynamic Shape)
        {
            using (Display.ViewDataContainer.Model.Node("Content"))
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
        public void ContentZone__api__Forms(dynamic Display, dynamic Shape) {

            using (Display.ViewDataContainer.Model.List("Zones")) {
                using (Display.ViewDataContainer.Model.Node("a-list-item")) {

                    Display.ViewDataContainer.Model.Set("Name", Shape.ZoneName);

                    using (Display.ViewDataContainer.Model.List("Items")) {

                        foreach (var item in (IEnumerable<dynamic>)Order(Shape)) {
                            Display(item);
                        }
                    }
                }
            }
        }

        [Shape(bindingType: "Translate")]
        public void List__api__Forms(dynamic Display, dynamic Shape) {
            using (Display.ViewDataContainer.Model.List("Items")) {
                foreach (var item in ((IEnumerable<dynamic>)Shape.Items)) {
                    Display(item);
                }
            }
        }


        // TitlePart
        [Shape(bindingType: "Translate")]
        public void Parts_Title__api__Forms(dynamic Display, dynamic Shape) {
            using (Display.ViewDataContainer.Model.Node("a-list-item")) {
                Display.ViewDataContainer.Model.Type = Shape.ContentPart.PartDefinition.Name;
                Display.ViewDataContainer.Model.Title = Shape.Title;
            }
        }

        [Shape(bindingType: "Translate")]
        public void Parts_Title_Summary__api__Forms(dynamic Display, dynamic Shape) {
            Parts_Title__api__Forms(Display, Shape);
        }

        // BodyPart
        [Shape(bindingType: "Translate")]
        public void Parts_Common_Body__api__Forms(dynamic Display, dynamic Shape) {
            using (Display.ViewDataContainer.Model.Node("a-list-item")) {
                Display.ViewDataContainer.Model.Type = Shape.ContentPart.PartDefinition.Name;
                Display.ViewDataContainer.Model.Html = Shape.Html.ToString();
            }
        }

        [Shape(bindingType: "Translate")]
        public void Parts_Common_Body_Summary__api__Forms(dynamic Display, dynamic Shape) {
            Parts_Common_Body__api__Forms(Display, Shape);
        }

        // MetadataPart
        [Shape(bindingType: "Translate")]
        public void Parts_Common_Metadata__api__Forms(dynamic Display, dynamic Shape) {
            System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(Display.ViewContext.RequestContext);

            using (Display.ViewDataContainer.Model.Node("a-list-item")) {
                Display.ViewDataContainer.Model.Type = Shape.ContentPart.PartDefinition.Name;
                Display.ViewDataContainer.Model.Id = Shape.ContentPart.Id;
                Display.ViewDataContainer.Model.ResourceUrl = urlHelper.ItemApiGet((IContent)Shape.ContentPart);
                Display.ViewDataContainer.Model.CreatedUtc = Shape.ContentPart.CreatedUtc;
                Display.ViewDataContainer.Model.PublishedUtc = Shape.ContentPart.PublishedUtc;
            }
        }

        [Shape(bindingType: "Translate")]
        public void Parts_Common_Metadata_Summary__api__Forms(dynamic Display, dynamic Shape) {
            Parts_Common_Metadata__api__Forms(Display, Shape);
        }
    }
}