#ifndef _SHOOTINGAPPLICATION_H_
#define _SHOOTINGAPPLICATION_H_

#include "GameApplication.h"
#include "SceneManager.h"

class ShootingApplication : public GameApplication
{
public:
	//コンストラクタ
	ShootingApplication( int argc, char *argv[] );

private:
	void initialize();
	void update( float frameTimer );
	void draw();
	void finish();

private:
	SceneManager scemeManager;
};

#endif