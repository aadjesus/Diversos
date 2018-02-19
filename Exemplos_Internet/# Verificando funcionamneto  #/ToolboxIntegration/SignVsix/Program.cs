// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="ComponentOwl.com">
//     Copyright © 2010-2012 ComponentOwl.com. All rights reserved.
// </copyright>
// <author>Libor Tinka</author>
// -----------------------------------------------------------------------

namespace SignVsix
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Packaging;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    #endregion

    internal class Program
    {
        private static void Main(string[] args)
        {
            // first argument - path to VSIX package
            string paramPathPackage = args[0].Replace("\"", "");
            // second argument - path to PFX certificate
            string paramPathCertificate = args[1].Replace("\"", "");
            // third argument - password for the certificate
            string paramPassword = args[2];

            // open VSIX package
            Package package = Package.Open(paramPathPackage, FileMode.Open);

            // load certificate
            byte[] certificate = File.ReadAllBytes(paramPathCertificate);

            // sign all parts of the package
            var signatureManager = new PackageDigitalSignatureManager(package)
            {
                CertificateOption = CertificateEmbeddingOption.InSignaturePart
            };

            List<Uri> partsToSign = new List<Uri>();

            foreach (PackagePart packagePart in package.GetParts())
            {
                partsToSign.Add(packagePart.Uri);
            }

            partsToSign.Add(PackUriHelper.GetRelationshipPartUri(signatureManager.SignatureOrigin));
            partsToSign.Add(signatureManager.SignatureOrigin);
            partsToSign.Add(PackUriHelper.GetRelationshipPartUri(new Uri("/", UriKind.RelativeOrAbsolute)));

            try
            {
                signatureManager.Sign(partsToSign, new X509Certificate2(certificate, paramPassword));
            }
            catch (CryptographicException cryptographicException)
            {
                Console.WriteLine("Signing failed: {0}", cryptographicException.Message);
            }
        }
    }
}