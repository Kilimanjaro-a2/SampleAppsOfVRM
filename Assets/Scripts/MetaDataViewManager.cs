using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
using UnityEngine.UI;
namespace KiliWare.SampleVRMApp
{
    public class MetaDataViewManager : MonoBehaviour
    {
        [SerializeField] protected Text _metaDataText;
        // Start is called before the first frame update
        void Awake()
        {
            var loadingManager = GetComponent<VRMLoadManager>();
            loadingManager.OnMetaDataLoaded += ShowMetaData;
        }

        protected void ShowMetaData(VRMMetaObject metaData)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("Name: ");
            sb.Append(metaData.Title);
            sb.Append("\n");

            sb.Append("Version: ");
            sb.Append(metaData.Version);
            sb.Append("\n");

            sb.Append("Author: ");
            sb.Append(metaData.Author);
            sb.Append("\n");

            sb.Append("ContactInformation: ");
            sb.Append(metaData.ContactInformation);
            sb.Append("\n");

            sb.Append("Reference: ");
            sb.Append(metaData.Reference);
            sb.Append("\n");

            sb.Append("AllowedUser: ");
            switch(metaData.AllowedUser)
            {
                case AllowedUser.OnlyAuthor:
                    sb.Append("OnlyAuthor");
                    break;
                case AllowedUser.ExplicitlyLicensedPerson:
                    sb.Append("ExplicitlyLicensedPerson");
                    break;
                case AllowedUser.Everyone:
                    sb.Append("Everyone");
                    break;
            }
            sb.Append("\n");

            sb.Append("ViolentUssage: ");
            sb.Append(metaData.ViolentUssage == UssageLicense.Allow ? "Allowed" : "Disallowed");
            sb.Append("\n");

            sb.Append("SexualUssage: ");
            sb.Append(metaData.SexualUssage == UssageLicense.Allow ? "Allowed" : "Disallowed");
            sb.Append("\n");

            sb.Append("CommercialUssage: ");
            sb.Append(metaData.CommercialUssage == UssageLicense.Allow ? "Allowed" : "Disallowed");
            sb.Append("\n");

            sb.Append("OtherPermissionUrl: ");
            sb.Append(metaData.OtherPermissionUrl);
            sb.Append("\n");

            _metaDataText.text = sb.ToString();
        }
    }
}
