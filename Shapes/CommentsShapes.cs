using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Morphous.Api;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;

namespace Morphous.Api.Native.Shapes
{
    public class CommentsShapes : ApiShapesBase, IShapeTableProvider
    {

        public void Discover(ShapeTableBuilder builder)
        {
        }

        [Shape(bindingType: "Translate")]
        public void Parts_ListOfComments(dynamic Display, dynamic Shape)
        {
            using (Display.ViewDataContainer.Model.Node(Shape.ContentPart))
            {
                Display.ViewDataContainer.Model.CommentsCount = Shape.ContentPart.CommentsCount;

                using (Display.ViewDataContainer.Model.List("Comments"))
                {
                    foreach (var item in ((IEnumerable<dynamic>)Shape.List))
                    {
                        Display(item);
                    }
                }
            }
        }

        [Shape(bindingType: "Translate")]
        public void Parts_CommentForm(dynamic Display, dynamic Shape)
        {

            using (Display.ViewDataContainer.Model.Node("CommentForm"))
            {
                Display.ViewDataContainer.Model.Type = "CommentEditor";

                Display(Shape.EditorShape);
            }
        }

        [Shape(bindingType: "Translate")]
        public void Content_Edit(dynamic Display, dynamic Shape)
        {

            //using (Display.ViewDataContainer.Model.Node("test"))
            //{
            //    Display.ViewDataContainer.Model.Type = "test";

            //    Display(Shape.Content);
            //}
        }



        [Shape(bindingType: "Translate")]
        public void Parts_Comment(dynamic Display, dynamic Shape)
        {
            Parts_Comment__api__Flat(Display, Shape);
        }

        [Shape(bindingType: "Translate")]
        public void Parts_Comment__api__Flat(dynamic Display, dynamic Shape)
        {
            using (Display.ViewDataContainer.Model.Node("CommentPart"))
            {
                Display.ViewDataContainer.Model.Type = Shape.ContentPart.PartDefinition.Name;
                Display.ViewDataContainer.Model.Author = Shape.ContentPart.Author;
                Display.ViewDataContainer.Model.CommentText = Shape.ContentPart.CommentText;
                using (Display.ViewDataContainer.Model.List("Comments"))
                {
                    DisplayChildren(Display, Shape);
                }
            }
        }
    }
}