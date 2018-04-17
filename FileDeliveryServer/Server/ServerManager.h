/*
 * ServerManager.h
 *
 *  Created on: 2018. 2. 11.
 *      Author: cmk
 */

#pragma once

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <netinet/in.h>
#include <unistd.h>
#include <pthread.h>

namespace server_manager {

class ServerManager {

public:
	static void* run(void* param){
		auto sm = static_cast<ServerManager*>(param);
		return sm->BeginServer();
	}

	ServerManager();
	virtual ~ServerManager();

private:



	void* BeginServer();

};

} /* namespace ctimer */
