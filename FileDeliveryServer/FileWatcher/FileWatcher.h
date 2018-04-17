/*
 * FileWatcher.h
 *
 *  Created on: 2018. 2. 12.
 *      Author: cmk
 */

#ifndef FILEWATCHER_FILEWATCHER_H_
#define FILEWATCHER_FILEWATCHER_H_

namespace file_watcher {

class FileWatcher {
public:
	FileWatcher();
	virtual ~FileWatcher();

	static void* run(void *param){
		return nullptr;
	}
};

} /* namespace ctimer */

#endif /* FILEWATCHER_FILEWATCHER_H_ */
