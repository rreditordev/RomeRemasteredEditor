using Model;
using System.Collections.Generic;
using Godot;

namespace Controller
{

public class CampaignResourcesController
{
	readonly Campaign _campaign;

	public CampaignResourcesController ()
	{
		_campaign = RtwDataContext.Campaign;
	}

	public List<ResourceDto> GetResourceDto ()
	{
		List<Model.Resource> resources = _campaign.GetResources();
		List<ResourceDto> dto = new();
		for (var i = 0; i < resources.Count; i++)
		{
			dto.Add(new ResourceDto
			{
				ResourceID = resources[i].ResourceID,
				AbundanceLevel = resources[i].AbundanceLevel,
				MapPosition = resources[i].MapPosition
			});
		}
		return dto;
	}

	public void SetResourceData (int index, ResourceDto newResourceData)
	{
		GD.PushWarning("SetResourceData not implemented");
	}
}

public struct ResourceDto
{
	public string ResourceID;
	public int AbundanceLevel;
	public Vector2I MapPosition;
}

}