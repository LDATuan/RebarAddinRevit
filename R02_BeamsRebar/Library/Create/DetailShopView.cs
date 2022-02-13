﻿using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DSP;
namespace R02_BeamsRebar
{
    public class DetailShopView
    {
        public ViewFamilyType Detail { get; set; }
        public BoundingBoxXYZ SectionBox { get; set; }
        public ViewSection DetailShop { get; set; }
        public ElementId Schedule { get; set; }
        public ViewSchedule ViewSchedule { get; set; }
        public string Type = "@BeamDetailShop";
        public DetailShopView()
        {
        }
        public void GetNameDetail(Document document)
        {
            List<ViewFamilyType> list = new FilteredElementCollector(document)
                                .OfClass(typeof(ViewFamilyType))
                                .Cast<ViewFamilyType>()
                                .Where(x => ViewFamily.Section == x.ViewFamily && x.Name.Equals(Type)).ToList();
            if (list.Count == 0)
            {
                ViewFamilyType a = new FilteredElementCollector(document)
                                .OfClass(typeof(ViewFamilyType))
                                .Cast<ViewFamilyType>()
                                .Where(x => ViewFamily.Section == x.ViewFamily).FirstOrDefault();
                Detail = a.Duplicate(Type) as ViewFamilyType;
            }
            else
            {
                Detail = list.Where(x => x.Name.Equals(Type)).FirstOrDefault();
            }

        }
        public void GetSectionBox(Document document, UnitProject unit, InfoModel infoModel, List<PlanarFace> planarFaces, List<Element> beams, double offset0, double offset01)
        {
            XYZ p1 = PointModel.ProjectToPlane(infoModel.LeftRightPlanar[0].Origin, infoModel.TopBottomPlanar[0]);
            XYZ p2 = p1 + unit.Convert(infoModel.b / 2) * (-1) * infoModel.LeftRightPlanar[0].FaceNormal;
            XYZ p3 = PointModel.ProjectToPlane(p2, planarFaces[0]);
            XYZ p4 = PointModel.ProjectToPlane(p2, planarFaces[planarFaces.Count - 1]);
            Line l = LineProcess.GetLineFromElement(beams[0]);
            List<Line> line = LineProcess.GetLine(beams, l);
            List<XYZ> point = LineProcess.GetPoint(line, l);
            XYZ p = point[0];
            XYZ q = point[1];
            //XYZ v = q - p;
            XYZ v = p4-p3;
            double length = 0;
            foreach (var item in line)
            {
                length += item.Length;
            }
            List<BoundingBoxXYZ> boundingBoxXYZs = new List<BoundingBoxXYZ>();
            foreach (var item in beams)
            {
                ElementType elementType = document.GetElement(item.GetTypeId()) as ElementType;
                boundingBoxXYZs.Add(item.get_BoundingBox(null));
            }

            double minZ = boundingBoxXYZs[0].Min.Z;
            double maxZ = boundingBoxXYZs[0].Max.Z;
            foreach (var item in boundingBoxXYZs)
            {
                if (minZ > item.Min.Z) minZ = item.Min.Z;
                if (maxZ < item.Max.Z) maxZ = item.Max.Z;
            }
            double w = length;
            double offset = unit.Convert(offset0);
            double offset1 = unit.Convert(offset01);
            XYZ min = new XYZ(-0.5 * w - offset, minZ - offset1, 0);
            XYZ max = new XYZ(0.5 * w + offset, maxZ + offset, offset);
            //XYZ a = p + 0.5 * v;
            XYZ a = p3 + 0.5 * v;
            XYZ midpoint = new XYZ(a.X, a.Y, v.Z);
            XYZ walldir = -v.Normalize();
            XYZ up = XYZ.BasisZ;
            XYZ viewdir = walldir.CrossProduct(up);
            Transform t = Transform.Identity;
            t.Origin = midpoint;
            t.BasisX = walldir;
            t.BasisY = up;
            t.BasisZ = viewdir;
            SectionBox = new BoundingBoxXYZ();
            SectionBox.Transform = t;
            SectionBox.Min = min;
            SectionBox.Max = max;
        }
        public void CeateDetailShopView(Document document, UnitProject unit, InfoModel infoModel, List<PlanarFace> planarFaces, List<Element> beams, SettingModel settingModel, string name, double offset0, double offset01)
        {
            GetNameDetail(document);
            GetSectionBox(document, unit, infoModel, planarFaces, beams, offset0,offset01);
            DetailShop = ViewSection.CreateSection(document, Detail.Id, SectionBox);
            try
            {
                DetailShop.Name = name;
            }
            catch (System.Exception)
            {
                MessageBox.Show("There is an existing Section View" +"\n"+ "Please choose another Beam Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DetailShop.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE).Set(0);
            DetailShop.ViewTemplateId = settingModel.SelectedDetailShopTemplate.Id;
        }
        public void CreateSchedule(Document document,SettingModel settingModel)
        {
            Schedule = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_DetailComponents).FirstElementId();
            if (Schedule!=null&&Schedule!=ElementId.InvalidElementId)
            {
                
                ViewSchedule = ViewSchedule.CreateSchedule(document, new ElementId(BuiltInCategory.OST_DetailComponents));
                document.Regenerate();
                ElementId image = new ElementId(BuiltInParameter.ALL_MODEL_IMAGE);
                ScheduleDefinition definition = ViewSchedule.Definition;
                SchedulableField schedulableFieldImage = definition.GetSchedulableFields().FirstOrDefault(sf => sf.ParameterId == image);

                if (schedulableFieldImage != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldImage);
                }
                Parameter elementHostParameter = new FilteredElementCollector(document).OfCategory(BuiltInCategory.OST_DetailComponents)
                    .WhereElementIsElementType().Cast<Parameter>().Where(x => x.Definition.Name.Equals("Element Host")).FirstOrDefault();
                if (elementHostParameter == null)
                {
                    MessageBox.Show("Test");
                }
                SchedulableField schedulableFieldElementHost = definition.GetSchedulableFields().FirstOrDefault(sf => sf.ParameterId == elementHostParameter.Id);
                if (schedulableFieldElementHost != null)
                {
                    // Add the found field
                    definition.AddField(schedulableFieldElementHost);
                }
            }
        }
        //public void AddRegularFieldToSchedule(Document document)
        //{
        //    ElementId image = new ElementId(BuiltInParameter.ALL_MODEL_IMAGE);
        //    ScheduleDefinition definition = ViewSchedule.Definition;
        //    SchedulableField schedulableFieldImage = definition.GetSchedulableFields().FirstOrDefault(sf => sf.ParameterId == image);
            
        //    if (schedulableFieldImage != null)
        //    {
        //        // Add the found field
        //        definition.AddField(schedulableFieldImage);
        //    }
            
        //}
    }
}
