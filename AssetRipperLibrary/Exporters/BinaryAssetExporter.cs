using AssetRipper.Core;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Parser.Files.SerializedFiles;
using AssetRipper.Core.Project;
using AssetRipper.Core.Project.Collections;
using AssetRipper.Core.Project.Exporters;
using System;
using System.Collections.Generic;
using Object = AssetRipper.Core.Classes.Object.Object;

namespace AssetRipper.Library.Exporters
{
	public class BinaryAssetExporter : IAssetExporter
	{
		public virtual bool IsHandle(UnityObjectBase asset)
		{
			return true;
		}

		public virtual bool Export(IExportContainer container, UnityObjectBase asset, string path)
		{
			throw new NotSupportedException();
		}

		public virtual void Export(IExportContainer container, UnityObjectBase asset, string path, Action<IExportContainer, UnityObjectBase, string> callback)
		{
			Export(container, asset, path);
			callback?.Invoke(container, asset, path);
		}

		public virtual bool Export(IExportContainer container, IEnumerable<UnityObjectBase> assets, string path)
		{
			throw new NotSupportedException();
		}

		public virtual void Export(IExportContainer container, IEnumerable<UnityObjectBase> assets, string path, Action<IExportContainer, UnityObjectBase, string> callback)
		{
			throw new NotSupportedException();
		}

		public virtual IExportCollection CreateCollection(VirtualSerializedFile virtualFile, UnityObjectBase asset)
		{
			return new AssetExportCollection(this, asset);
		}

		public AssetType ToExportType(UnityObjectBase asset)
		{
			ToUnknownExportType(asset.ClassID, out AssetType assetType);
			return assetType;
		}

		public bool ToUnknownExportType(ClassIDType classID, out AssetType assetType)
		{
			assetType = AssetType.Meta;
			return true;
		}

		protected static bool IsValidData(byte[] data) => data != null && data.Length > 0;
	}
}
