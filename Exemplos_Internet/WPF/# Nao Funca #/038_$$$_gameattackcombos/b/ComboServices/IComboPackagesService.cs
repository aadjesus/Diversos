using System.ServiceModel;

namespace GG.GameAttackCombos.Services {

	/// <summary>
	/// The contract of the service for requesting game attack combo packages for download.
	/// </summary>
	[ServiceContract(Namespace = "http://gurugames.com/packages/")]
	public interface IComboPackagesService {

		[OperationContract]
		byte[] DownloadComboPackage(string gameCode, string clientPackageVersion);

	}

}