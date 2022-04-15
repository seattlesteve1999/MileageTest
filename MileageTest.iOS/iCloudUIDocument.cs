using Foundation;
using MobileCoreServices;
using System;
using UIKit;

namespace MileageManager.iOS
{
    public class iCloudUIDocument : UIDocument
    {

        // the 'model', just a chunk of text in this case; must easily convert to NSData
        NSString dataModel;
        // model is wrapped in a nice .NET-friendly property
        public string DocumentString
        {
            get
            {
                return dataModel.ToString();
            }
            set
            {
                dataModel = new NSString(value);
            }
        }
        public iCloudUIDocument(NSUrl url) : base(url)
        {
            DocumentString = "MileageManagerDadsiPhone02092020";
        }
        // contents supplied by iCloud to display, update local model and display (via notification)
        public override bool LoadFromContents(NSObject contents, string typeName, out NSError outError)
        {
            NSString uTType = UTType.Content;
            outError = null;

            Console.WriteLine("LoadFromContents({0})", typeName);

            if (contents != null)
                dataModel = NSString.FromData(contents.ToString(), NSStringEncoding.UTF8);

            // LoadFromContents called when an update occurs
            NSNotificationCenter.DefaultCenter.PostNotificationName("monkeyDocumentModified", this);
            return true;
        }
        // return contents for iCloud to save (from the local model)
        public override NSObject ContentsForType(string typeName, out NSError outError)
        {
            outError = null;

            Console.WriteLine("ContentsForType({0})", typeName);
            Console.WriteLine("DocumentText:{0}", dataModel);

            NSData docData = dataModel.Encode(NSStringEncoding.UTF8);
            return docData;
        }
    }
}