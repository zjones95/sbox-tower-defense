using Sandbox;
using Sandbox.Diagnostics;
using Sandbox.Internal;
using System.Collections.Generic;

namespace MyGame;

public partial class Selector : EntityComponent<Pawn>, ISingletonComponent
{
	protected AnimatedEntity GhostTower;
	public bool ShowGhostTower = false;
	public bool CanPlaceTower = false;

	public List<Entity> TowerList = new List<Entity>();

	public void PlaceTower( IClient cl )
	{
		{
			var ray = cl.Pawn.AimRay;

			var rayTrace = Trace.Ray( ray.Position, ray.Position + ray.Forward * 2500 ).Ignore( cl.Pawn ).WorldOnly();

			var towerPosition = rayTrace.Run().EndPosition;
			
			if (ShowGhostTower)
			{
				GhostTower.Position = towerPosition;

				if ( GhostTower.LocalPosition.z > 0)
				{
					CanPlaceTower = false;
					GhostTower.RenderColor = Color.Red;
				}
				else
				{
					CanPlaceTower = true;
					GhostTower.RenderColor = Color.Green;
				}

				if( Input.Released( InputButton.PrimaryAttack ) && CanPlaceTower )
				{
					var tower = new Tower();
					TowerList.Add( tower );
					tower.Position = new Vector3(towerPosition);
				}
			}

			

			if ( Input.Released( InputButton.Slot1 ) )
			{
				ShowGhostTower = !ShowGhostTower;

				if ( ShowGhostTower )
				{
					GhostTower = new GhostTower();
				}
				else
				{
					GhostTower.Delete();
				}
			}
		}
	}

	public void Simulate( IClient cl )
	{
		PlaceTower( cl );
	}
}
