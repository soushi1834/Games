#ifndef _CREDIT_SCENE_H_
#define _CREDIT_SCENE_H_

#include "IScene.h"

class Credit : public IScene
{
public:
	Credit();
	~Credit();

	virtual void initialize();

	virtual void update(float time);

	virtual void draw();

	virtual bool isEnd() const;

	virtual SceneID nextScene() const;

	virtual void end() const;
};

#endif