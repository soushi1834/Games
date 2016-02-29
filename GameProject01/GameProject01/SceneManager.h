#ifndef _SCENE_MANAGER_H_
#define _SCENE_MANAGER_H_

#include "IScene.h"
#include <memory>
#include <map>
#include "SceneID.h"

class SceneManager
{
public:
	SceneManager();
	~SceneManager();

	void initialize();

	void update(float time);

	void draw();

	void changeScene(SceneID id);

	void exit();

	void add(SceneID id, IScene* scene);

private:
	//ÉRÉsÅ[ã÷é~
	SceneManager(const SceneManager& other);
	SceneManager& operator = (const SceneManager& other);

	typedef std::shared_ptr<IScene>	ScenePtr;
	typedef std::map<SceneID, ScenePtr> SceneContainer;
	ScenePtr mScene;
	SceneContainer scenes;
};

#endif