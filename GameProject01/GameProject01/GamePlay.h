#ifndef _GAME_PLAY_H_
#define _GAME_PLAY_H_

#include "IScene.h"

class GamePlay : public IScene
{
public:
	GamePlay();
	~GamePlay();

	virtual void initialize();

	virtual void update(float time);

	virtual void draw();

	virtual bool isEnd() const;

	virtual SceneID nextScene() const;

	virtual void end() const;
};

#endif