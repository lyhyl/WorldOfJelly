==============================
JellyXNADraw.cs

	<---Code--->
	protected const int DivideIter = 2;
	<---Code--->

	if change this value
	remember update the code in JellyObject.cs

	<---Code--->
		private void UpdateXNAVertex()
		{
			int i = 0;
			for (i = 1; i < _crvedgebuffer.Length; ++i)//offest ! ( here is 1 )
				SetVertexPosition(i, _crvedgebuffer[i - 1]);
	
			SetVertexPosition(0, _crvedgebuffer[i - 1]);//write the rest elements to begin position
	
			foreach (int id in _extverid)
			SetVertexPosition(i++, _nodes[id].Position);

			FlushVertex();
		}
	<---Code--->
==============================

==============================
NOTE - Refactoring
------------------------------------------------
Now the control use UIResourceManager.SpriteBatch
	and Game use it as well.
But you must Begin/End Draw Sprite in Game

Use ResourceManager SpriteBatch only
move method to ResourceManager
&
rename UIResourceManager to ResourceManager