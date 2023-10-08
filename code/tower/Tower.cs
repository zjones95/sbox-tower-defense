using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame;

partial class Tower : AnimatedEntity
{
	public override void Spawn()
	{
		base.ClientSpawn();
		SetModel( "models/citizen/citizen.vmdl" );

		Scale = 1f;
		RenderColor = Color.Gray;
	}
}
