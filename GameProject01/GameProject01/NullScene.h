#ifndef _NULL_SCENE_H_
#define _NULL_SCENE_H_

#include "IScene.h"

class NullScene : public IScene
{
public:
	NullScene();
	~NullScene();

	virtual void initialize();

	virtual void update(float time);

	virtual void draw();

	virtual bool isEnd() const;

	virtual SceneID nextScene() const;

	virtual void end() const;
};

#endif