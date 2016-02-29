#ifndef _SOUND_H_
#define _SOUND_H_

#include <gslib.h>
#include <map>
#include "SoundID.h"

class WaveSound
{
public:
	WaveSound();
	~WaveSound();

	void addSound(const SoundID id, const char* filename, int count);

	void playSound(const SoundID id);

	void playSoundOnce(const SoundID id);

	void playSoundLoop(const SoundID id);

	void stopSound(const SoundID id);

	void deleteSound(const SoundID id);

	bool isPlaying(const SoundID id);

	void soundOnceFlagReset(SoundID id);

private:
	std::map<SoundID, bool> soundsflag;
};

#endif