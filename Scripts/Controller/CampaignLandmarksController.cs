using Model;
using System.Collections.Generic;
using Godot;

namespace Controller
{

public class CampaignLandmarksController
{
	readonly Campaign _campaign;

	public CampaignLandmarksController ()
	{
		_campaign = RtwDataContext.Campaign;
	}

	public List<LandmarkDto> GetLandmarksDto ()
	{
		List<Landmark> landmarks = _campaign.GetLandmarks();
		List<LandmarkDto> dto = new();
		for (var i = 0; i < landmarks.Count; i++)
		{
			Landmark landmark = landmarks[i];
			dto.Add(new LandmarkDto
			{
				LandmarkID = landmark.ID,
				MapPosition = landmark.MapPosition
			});
		}
		return dto;
	}
}

public struct LandmarkDto
{
	public string LandmarkID;
	public Vector2I MapPosition;
}

}