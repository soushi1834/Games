#ifndef _WORLD_H_
#define _WORLD_H_

#include "IWorld.h"
#include "WorldCharacter.h"

class World : public IWorld
{
public:
	World();
	World(WaveSound se);

	~World();

	void update(float time);

	void draw();

	//消去
	void clear();

	//キャラクターの追加
	virtual void add(CharacterID id, const CharacterPtr& character);

	//プレイヤーの追加
	void addPlayer(const PlayerPtr& player);

	//プレイヤーの取得
	virtual const CharacterPtr& getPlayer() const;

	//プレイヤーの取得(Playerクラス)
	const PlayerPtr& getPlayerParam() const;

	//画面内にいるかどうか
	virtual bool isInSide(const GSvector2& position) const;

	virtual const bool isBossDead();

	virtual void playSE(SoundID id);

private:
	CharacterPtr player_;
	PlayerPtr playerParam_;
	WorldCharacter characters_;
};

#endif