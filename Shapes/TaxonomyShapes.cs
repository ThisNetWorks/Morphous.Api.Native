using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Morphous.Api;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;

namespace Morphous.Api.Native.Shapes {
    public class TaxonomyShapes : ApiShapesBase, IShapeTableProvider {

        public void Discover(ShapeTableBuilder builder) {
        }

        [Shape(bindingType: "Translate")]
        public void Parts_TaxonomyPart__api__Native(dynamic Display, dynamic Shape) {

            using (Display.ViewDataContainer.Model.Node("a-list-item")) {
                Display.ViewDataContainer.Model.Type = Shape.ContentPart.PartDefinition.Name;
                
                Display(Shape.Taxonomy);
            }
        }

        //[Shape(bindingType: "Translate")]
        //public void Taxonomy__api__Native(dynamic Display, dynamic Shape)
        //{
        //    using (Display.ViewDataContainer.Model.List("Terms"))
        //    {
        //        foreach (var item in ((IEnumerable<dynamic>)Shape.Terms))
        //        {
        //            Display(item);
        //        }
        //    }
        //}

        //[Shape(bindingType: "Translate")]
        //public void TaxonomyItem__api__Native(dynamic Display, dynamic Shape)
        //{
        //    Shape.Metadata.Type = "Content";
        //    Display(Shape);
        //}

        [Shape(bindingType: "Translate")]
        public void Parts_TermPart__api__Native(dynamic Display, dynamic Shape) {

            using (Display.ViewDataContainer.Model.Node("a-list-item")) {
                Display.ViewDataContainer.Model.Type = Shape.ContentPart.PartDefinition.Name;
                Display(Shape.ContentItems);
                Display(Shape.Pager);
            }
        }
    }
}