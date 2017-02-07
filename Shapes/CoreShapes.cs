using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Morphous.Api;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;

namespace Morphous.FormsApi.Shapes {
    public class CoreShapes : ApiShapesBase, IShapeTableProvider {

        public void Discover(ShapeTableBuilder builder) {

        }

        [Shape(bindingType: "Translate")]
        public void ContentZone__api__Forms(dynamic Display, dynamic Shape) {

            using (Display.ViewDataContainer.Model.Node(Shape.ZoneName)) {

                foreach (var item in (IEnumerable<dynamic>)Order(Shape)) {
                    Display(item);
                }
            }
        }
    }
}