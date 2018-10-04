using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class UserData
{
	[XmlAttribute("name")]
	public string Name;

	[XmlAttribute("score")]
	public int Score;

	[XmlAttribute("exitReason")]
	public string ExitReason;

	[XmlAttribute("playedTime")]
	public string PlayedTime;

	[XmlAttribute("launchDate")]
	public string LaunchDate;
}

[XmlRoot("usersContainer")]
public class UsersContainer 
{
	[XmlArray("users")]
	[XmlArrayItem("user")]
	public List<UserData> Users = new List<UserData>();
}
