#include "ShootingApplication.h"
#include "Screen.h"
#include "Title.h"
#include "GamePlay.h"
#include "Credit.h"
#include <gslib.h>

//fps‚Á‚Û‚¢‚à‚Ì—p
int frame;
float basetime;
float nowtime;
float fps;

ShootingApplication::ShootingApplication( int argc, char *argv[] ):
	GameApplication( argc, argv )
	{		
		setWindowWidth( SCREEN_WIDTH );
		setWindowHeight ( SCREEN_HEIGHT );
	}

	void ShootingApplication::initialize()
	{
		scemeManager.initialize();
		scemeManager.add(SCENE_TITLE, new Title());
		scemeManager.add(SCENE_GAMEPLAY, new GamePlay());
		scemeManager.add(SCENE_CREDIT, new Credit());
		scemeManager.changeScene(SCENE_TITLE);
	}

	void ShootingApplication::update( float frameTimer )
	{
		scemeManager.update(frameTimer);
	}

	void ShootingApplication::draw()
	{
		scemeManager.draw();

		//fps‚Á‚Û‚¢‚à‚Ì
		frame += 1;
		nowtime = clock();
		if (nowtime - basetime > 1000)
		{
			fps = frame * 1000.0 / (nowtime - basetime);
			basetime = nowtime;
			frame = 0;
		}
		int fps_ = (fps - (int)fps) * 10;
		gsFontParameter(1, 24, "‚l‚r –¾’©");
		gsTextPos(900, 700);
		gsDrawText("%i.%i fps", (int)fps, fps_);
	}

	void ShootingApplication::finish()
	{
		scemeManager.exit();
	}