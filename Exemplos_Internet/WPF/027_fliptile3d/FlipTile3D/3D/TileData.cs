using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows;

namespace FlipTile3D
{
    /// <summary>
    /// Source code origin Kevin Moore, Kevin's WPF Bag-o-Tricks
    /// http://j832.com/bagotricks/
    /// 
    /// Modified by Sacha Barber for codeproject article
    /// </summary>
    internal class TileData
    {
        #region Data

        private Point3D locationDesired;
        private Size workingSize;
        private Point3D locationCurrent;
        private Vector3D locationVelocity;
        private double scaleDesired = 1;
        private double scaleCurrent = 1;
        private double scaleVelocity = 0;
        private readonly double weird = Rnd.NextDouble() * .1 + .85;
        private Quaternion rotationCurrent = new Quaternion();
        private Quaternion rotationVelocity = new Quaternion();
        private double flipVerticalVelocity = 0;
        private readonly Model3DGroup quad = new Model3DGroup();
        private readonly TranslateTransform3D translate = new TranslateTransform3D();
        private readonly QuaternionRotation3D quaternionRotation3D = new QuaternionRotation3D();
        private readonly AxisAngleRotation3D verticalFlipRotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), Rnd.NextDouble() * 360 + 720);
        private readonly ScaleTransform3D scaleTransform = new ScaleTransform3D();
        private readonly System.Windows.Media.Media3D.DiffuseMaterial borderMaterial = new DiffuseMaterial(Brushes.Black);
        internal System.Windows.Media.Media3D.DiffuseMaterial diffuseMaterial;
        internal String disciplesBlogUri;
        internal const double Diff = .000001;
        internal static readonly Random Rnd = new Random();

        #endregion

        #region Public Methods
        public bool TickData(Vector lastMouse, bool isFlipped)
        {
            bool somethingChanged = false;

            //active means nothing in the "flipped" mode
            bool isActiveItem = isActive(locationDesired, workingSize, (Point)lastMouse) && !isFlipped;
            bool goodMouse = !isEmptyPoint((Point)lastMouse);

            #region rotation

            Quaternion rotationTarget = new Quaternion(new Vector3D(1, 0, 0), 0);

            //apply forces
            rotationTarget.Normalize();
            rotationCurrent.Normalize();


            double angle = 0;
            Vector3D axis = new Vector3D(0, 0, 1);
            if (!double.IsNaN(lastMouse.X) && !isFlipped)
            {
                Point3D mouse = new Point3D(lastMouse.X, lastMouse.Y, 1);
                Vector3D line = mouse - locationCurrent;
                Vector3D straight = new Vector3D(0, 0, 1);

                angle = Vector3D.AngleBetween(line, straight);
                axis = Vector3D.CrossProduct(line, straight);
            }
            Quaternion rotationForceTowardsMouse = new Quaternion(axis, -angle);

            Quaternion rotationForceToDesired = rotationTarget - rotationCurrent;

            Quaternion rotationForce = rotationForceToDesired + rotationForceTowardsMouse;


            rotationVelocity *= new Quaternion(rotationForce.Axis, rotationForce.Angle * .2);

            //dampenning
            rotationVelocity = new Quaternion(rotationVelocity.Axis, rotationVelocity.Angle * (weird - .3));

            //apply terminal velocity
            rotationVelocity = new Quaternion(rotationVelocity.Axis, rotationVelocity.Angle);

            rotationVelocity.Normalize();

            //apply to position
            rotationCurrent *= rotationVelocity;
            rotationCurrent.Normalize();

            //see if there is any real difference between what we calculated and what actually exists
            if (AnyDiff(quaternionRotation3D.Quaternion.Axis, rotationCurrent.Axis, Diff) ||
                AnyDiff(quaternionRotation3D.Quaternion.Angle, rotationCurrent.Angle, Diff))
            {
                //if the angles are both ~0, the axis may be way off but the result is basically the same
                //check for this and forget animating in this case
                if (AnyDiff(quaternionRotation3D.Quaternion.Angle, 0, Diff) || AnyDiff(rotationCurrent.Angle, 0, Diff))
                {
                    quaternionRotation3D.Quaternion = rotationCurrent;
                    somethingChanged = true;
                }
            }


            #endregion

            #region flip
            double verticalFlipTarget = isFlipped ? 180 : 0;
            double verticalFlipCurrent = verticalFlipRotation.Angle;

            //force
            double verticalFlipForce = verticalFlipTarget - verticalFlipCurrent;

            //velocity
            flipVerticalVelocity += .3 * verticalFlipForce;

            //dampening
            flipVerticalVelocity *= (weird - .3);

            //terminal velocity
            flipVerticalVelocity = limitDouble(flipVerticalVelocity, 10);

            //apply
            verticalFlipCurrent += flipVerticalVelocity;

            if (AnyDiff(verticalFlipCurrent, verticalFlipRotation.Angle, Diff) && AnyDiff(flipVerticalVelocity, 0, Diff))
            {
                verticalFlipRotation.Angle = verticalFlipCurrent;
            }

            #endregion

            #region scale
            if (isActiveItem && !isFlipped)
            {
                this.scaleDesired = 2;
            }
            else
            {
                this.scaleDesired = 1;
            }

            double scaleForce = this.scaleDesired - this.scaleCurrent;
            this.scaleVelocity += .1 * scaleForce;
            //dampening
            this.scaleVelocity *= .8;
            //terminal velocity
            this.scaleVelocity = limitDouble(this.scaleVelocity, .05);
            this.scaleCurrent += this.scaleVelocity;

            if (AnyDiff(scaleTransform.ScaleX, scaleCurrent, Diff) || AnyDiff(scaleTransform.ScaleY, scaleCurrent, Diff))
            {
                this.scaleTransform.ScaleX = this.scaleCurrent;
                this.scaleTransform.ScaleY = this.scaleCurrent;
                somethingChanged = true;
            }

            #endregion

            #region location
            Vector3D locationForce;

            //apply forces
            if (isActiveItem)
            {
                locationDesired.Z = .1;
            }
            else
            {
                locationDesired.Z = 0;
            }
            locationForce = locationDesired - locationCurrent;

            //only repel the non-active items
            if (!isActiveItem && goodMouse && !isFlipped)
            {
                locationForce += .025 * invertVector(this.CurrentLocationVector - new Vector3D(lastMouse.X, lastMouse.Y, 0));
            }

            locationVelocity += .1 * locationForce;

            //apply dampenning
            locationVelocity *= (weird - .3);

            //apply terminal velocity
            locationVelocity = limitVector3D(locationVelocity, .3);

            //apply velocity to location
            locationCurrent += locationVelocity;

            if ((GetVector(translate) - (Vector3D)locationCurrent).Length > Diff)
            {
                translate.OffsetX = locationCurrent.X;
                translate.OffsetY = locationCurrent.Y;
                translate.OffsetZ = locationCurrent.Z;
                somethingChanged = true;
            }
            #endregion

            if(isFlipped)
            {
                Boolean t = true;
            }


            return somethingChanged;
        }

        public static Vector3D GetVector(TranslateTransform3D transform)
        {
            return new Vector3D(transform.OffsetX, transform.OffsetY, transform.OffsetZ);
        }

        public void Setup3DItem(String discipleUri,
            Model3DGroup targetGroup, DiffuseMaterial diffuseMaterialBrushPair,
            Size size, Point center, Material backMaterial, Rect backTextureCoordinates)
        {
            locationDesired = new Point3D(center.X, center.Y, 0);
            locationCurrent = new Point3D(0, 0, Rnd.NextDouble() * 10 - 20);
            workingSize = size;

            Point3D topLeft = new Point3D(-size.Width / 2, size.Height / 2, 0);
            Point3D topRight = new Point3D(size.Width / 2, size.Height / 2, 0);
            Point3D bottomLeft = new Point3D(-size.Width / 2, -size.Height / 2, 0);
            Point3D bottomRight = new Point3D(size.Width / 2, -size.Height / 2, 0);

            diffuseMaterial = diffuseMaterialBrushPair;
            disciplesBlogUri = discipleUri;




            quad.Children.Add(
                CreateTile(
                    diffuseMaterialBrushPair,
                    backMaterial,
                    borderMaterial,
                    new Size3D(size.Width, size.Height, .01),
                    backTextureCoordinates));

            Transform3DGroup group = new Transform3DGroup();

            group.Children.Add(new RotateTransform3D(verticalFlipRotation));
            group.Children.Add(new RotateTransform3D(this.quaternionRotation3D));

            group.Children.Add(scaleTransform);
            group.Children.Add(translate);

            quad.Transform = group;

            targetGroup.Children.Add(quad);
        }
        #endregion

        #region Private Methods
        private static bool AnyDiff(double d1, double d2, double diff)
        {
            Debug.Assert(AreGoodNumbers(d1, d2, diff));
            Debug.Assert(diff >= 0);
            return Math.Abs(d1 - d2) > diff;
        }
        private static bool AnyDiff(Vector3D v1, Vector3D v2, double diff)
        {
            Debug.Assert(IsGoodNumber(diff));
            Debug.Assert(diff >= 0);
            double angleBetween = Vector3D.AngleBetween(v1, v2);
            return angleBetween > diff;
        }

        private static bool IsGoodNumber(double d)
        {
            return !double.IsNaN(d) && !double.IsInfinity(d);
        }
        private static bool AreGoodNumbers(params double[] d)
        {
            for (int i = 0; i < d.Length; i++)
            {
                if (!IsGoodNumber(d[i]))
                {
                    return false;
                }
            }
            return true;
        }


        private static Model3DGroup CreateTile(Material frontMaterial, Material backMaterial, Material sideMaterial,
            Size3D size, Rect backMaterialCoordiantes)
        {
            //these are represent half the width, height, depth of the quads, since everything is from zero
            double w = size.X / 2;
            double h = size.Y / 2;
            double d = size.Z / 2;

            //front
            GeometryModel3D front = GetQuad(
                new Point3D(-w, -h, d),
                new Point3D(w, -h, d),
                new Point3D(w, h, d),
                new Point3D(-w, h, d),
                frontMaterial);

            //back
            GeometryModel3D back = GetQuad(
                new Point3D(-w, -h, d),
                new Point3D(w, -h, d),
                new Point3D(w, h, d),
                new Point3D(-w, h, d),
                backMaterial, backMaterialCoordiantes);

            RotateTransform3D backRotate =
                new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 180), new Point3D());
            RotateTransform3D backFlip =
                new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 180), new Point3D());

            Transform3DGroup backTransformGroup = new Transform3DGroup();
            backTransformGroup.Children.Add(backRotate);
            backTransformGroup.Children.Add(backFlip);

            back.Transform = backTransformGroup;

            GeometryModel3D bottom, left, top, right;
            //sides
            {
                //right
                right = GetQuad(
                    new Point3D(w, -h, d),
                    new Point3D(w, -h, -d),
                    new Point3D(w, h, -d),
                    new Point3D(w, h, d), sideMaterial);


                //left
                left = GetQuad(
                    new Point3D(-w, -h, -d),
                    new Point3D(-w, -h, d),
                    new Point3D(-w, h, d),
                    new Point3D(-w, h, -d), sideMaterial);


                //top
                top = GetQuad(
                    new Point3D(-w, h, d),
                    new Point3D(w, h, d),
                    new Point3D(w, h, -d),
                    new Point3D(-w, h, -d), sideMaterial);


                //bottom
                bottom = GetQuad(
                    new Point3D(-w, -h, -d),
                    new Point3D(w, -h, -d),
                    new Point3D(w, -h, d),
                    new Point3D(-w, -h, d), sideMaterial);

            }

            Model3DGroup group = new Model3DGroup();
            group.Children.Add(front);
            group.Children.Add(back);
            group.Children.Add(right);
            group.Children.Add(left);
            group.Children.Add(bottom);
            group.Children.Add(top);

            return group;
        }

        private static GeometryModel3D GetQuad(
            Point3D bottomLeft, Point3D bottomRight, Point3D topRight, Point3D topLeft,
            Material material)
        {
            return GetQuad(bottomLeft, bottomRight, topRight, topLeft, material, new Rect(0, 0, 1, 1));
        }

        private static GeometryModel3D GetQuad(
            Point3D bottomLeft, Point3D bottomRight, Point3D topRight, Point3D topLeft,
            Material material, Rect textureCoordinates)
        {

            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(bottomLeft);
            mesh.Positions.Add(bottomRight);
            mesh.Positions.Add(topRight);
            mesh.Positions.Add(topLeft);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);

            mesh.TextureCoordinates.Add(textureCoordinates.BottomLeft);
            mesh.TextureCoordinates.Add(textureCoordinates.BottomRight);
            mesh.TextureCoordinates.Add(textureCoordinates.TopRight);
            mesh.TextureCoordinates.Add(textureCoordinates.TopLeft);

            GeometryModel3D gm3d = new GeometryModel3D(mesh, material);
            gm3d.BackMaterial = material;

            return gm3d;
        }

        private static bool isActive(Point3D center, Size size, Point mouse)
        {
            Point bottomLeft = new Point(center.X - size.Width / 2, center.Y - size.Height / 2);
            Point topRight = new Point(center.X + size.Width / 2, center.Y + size.Height / 2);

            Vector blDiff = mouse - bottomLeft;
            Vector trDiff = topRight - mouse;

            return blDiff.X >= 0 && blDiff.Y >= 0 && trDiff.X >= 0 && trDiff.Y >= 0;
        }

        private static bool isEmptyPoint(Point point)
        {
            return (double.IsNaN(point.X) && double.IsNaN(point.Y));
        }

        private static Vector3D invertVector(Vector3D input)
        {
            double invertLength = 1 / input.Length;
            input.Normalize();
            return invertLength * input;
        }

        private static Vector3D limitVector3D(Vector3D input, double max)
        {
            Debug.Assert(max > 0);
            Debug.Assert(!double.IsPositiveInfinity(max));
            Debug.Assert(!double.IsNaN(input.Length));

            if (input.Length > max)
            {
                input.Normalize();
                return input * max;
            }
            else
            {
                return input;
            }
        }

        private static double limitDouble(double input, double max)
        {
            Debug.Assert(max >= 0);

            if (Math.Abs(input) > max)
            {
                return Math.Sign(input) * max;
            }
            else
            {
                return input;
            }
        }
        #endregion

        #region Properties

        private Vector3D CurrentLocationVector
        {
            get
            {
                return new Vector3D(locationCurrent.X, locationCurrent.Y, locationCurrent.Z);
            }
        }

        #endregion
    }
}
