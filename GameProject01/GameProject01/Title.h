#ifndef _TITLE_H_
#define _TITLE_H_

#include "IScene.h"

enum PHASE
{
	PHASE_PUSH_START,
	PHASE_SELECT_SCENE
};

enum SELECT_SCENE
{
	GAME_PLAY,
	CREDIT
};

enum SELECT_STAGE
{
	STAGE_1
};

class Title : public IScene
{
public:
	Title();
	~Title();

	virtual void initialize();

	virtual void update(float time);

	virtual void draw();

	virtual bool isEnd() const;

	virtual SceneID nextScene() const;

	virtual void end() const;

private:
	PHASE phase;
	SELECT_SCENE selectScene;
	SELECT_STAGE selectStage;
};

#endif