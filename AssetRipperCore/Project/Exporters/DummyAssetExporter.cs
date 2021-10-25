using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Parser.Files.SerializedFiles;
using AssetRipper.Core.Project.Collections;
using System;
using System.Collections.Generic;
using Object = AssetRipper.Core.Classes.Object.Object;

namespace AssetRipper.Core.Project.Exporters
{
	internal class DummyAssetExporter : IAssetExporter
	{
		public void SetUpClassType(ClassIDType classType, bool isEmptyCollection, bool isMetaType)
		{
			m_emptyTypes[classType] = isEmptyCollection;
			m_metaTypes[classType] = isMetaType;
		}

		public bool IsHandle(UnityObjectBase asset)
		{
			return true;
		}

		public bool Export(IExportContainer container, UnityObjectBase asset, string path)
		{
			throw new NotSupportedException();
		}

		public void Export(IExportContainer container, UnityObjectBase asset, string path, Action<IExportContainer, UnityObjectBase, string> callback)
		{
			throw new NotSupportedException();
		}

		public bool Export(IExportContainer container, IEnumerable<UnityObjectBase> assets, string path)
		{
			throw new NotSupportedException();
		}

		public void Export(IExportContainer container, IEnumerable<UnityObjectBase> assets, string path, Action<IExportContainer, UnityObjectBase, string> callback)
		{
			throw new NotSupportedException();
		}

		public IExportCollection CreateCollection(VirtualSerializedFile virtualFile, UnityObjectBase asset)
		{
			if (m_emptyTypes.TryGetValue(asset.ClassID, out bool isEmptyCollection))
			{
				if (isEmptyCollection)
				{
					return new EmptyExportCollection();
				}
				else
				{
					return new SkipExportCollection(this, asset);
				}
			}
			else
			{
				throw new NotSupportedException(asset.ClassID.ToString());
			}
		}

		public AssetType ToExportType(UnityObjectBase asset)
		{
			ToUnknownExportType(asset.ClassID, out AssetType assetType);
			return assetType;
		}

		public bool ToUnknownExportType(ClassIDType classID, out AssetType assetType)
		{
			if (m_metaTypes.TryGetValue(classID, out bool isMetaType))
			{
				assetType = isMetaType ? AssetType.Meta : AssetType.Serialized;
				return true;
			}
			else
			{
				throw new NotSupportedException(classID.ToString());
			}
		}

		private readonly Dictionary<ClassIDType, bool> m_emptyTypes = new Dictionary<ClassIDType, bool>();
		private readonly Dictionary<ClassIDType, bool> m_metaTypes = new Dictionary<ClassIDType, bool>();
	}
}
