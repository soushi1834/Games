#ifndef _CHARACTER_H_
#define _CHARACTER_H_

#include "TextureID.h"
#include "CharacterID.h"
#include <gslib.h>

class IWorld;

class Character
{
public:
	Character(IWorld& world, CharacterID characterId, TextureID textureId, const GSvector2& position, float radius);

	virtual ~Character() {}

	virtual void update(float time) = 0;

	virtual void draw() const = 0;

	//死亡しているか
	bool isDead() const;

	//衝突判定
	void collide(Character& other);

	//衝突しているか
	bool isCollide(const Character& other) const;

	//方向を返す
	GSvector2 direction(const Character& other) const;

	//場所を返す
	GSvector2 getPosition();

	//キャラのIDを返す
	CharacterID getCharacterID();

protected:
	//衝突イベント
	virtual void onCollide(Character& other);

private:
	//コピー禁止
	Character(const Character& other);
	Character& operator = (const Character& other);

protected:
	IWorld& world_;				//ワールド
	CharacterID characterId_;	//キャラクターID
	TextureID textureId_;				//テクスチャID
	GSvector2 position_;		//座標
	GSvector2 velosity_;		//移動量
	float radius_;				//衝突判定半径
	bool alive_;				//生きているか
	float timer_;				//タイマー
};

#endif