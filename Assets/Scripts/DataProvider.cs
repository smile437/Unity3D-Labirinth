using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;


public class DataProvider 
{
	public static void Serializer(UsersContainer dataToSave)
	{
		var serializer = new XmlSerializer(typeof(UsersContainer));
		var stream = new FileStream(Application.dataPath + "/Data.xml", FileMode.Create);
		serializer.Serialize(stream, dataToSave);
		stream.Close();
	}
		
	public static List<UserData> Deserializer()
	{
		if (File.Exists (Application.dataPath + "/Data.xml")) 
		{
			var serializer = new XmlSerializer (typeof(UsersContainer));
			var stream = new FileStream (Application.dataPath + "/Data.xml", FileMode.OpenOrCreate);
			UsersContainer container = serializer.Deserialize (stream) as UsersContainer;
			stream.Close ();
			return container.Users;
		} 
		else
		{
			return new List<UserData>();
		}
	}
}
