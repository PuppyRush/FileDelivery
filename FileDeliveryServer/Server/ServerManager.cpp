/*
 * ServerManager.cpp
 *
 *  Created on: 2018. 2. 11.
 *      Author: cmk
 */

#include "../Server/ServerManager.h"
using namespace server_manager;

ServerManager::ServerManager() {
	// TODO Auto-generated constructor stub

}

ServerManager::~ServerManager() {
	// TODO Auto-generated destructor stub
}

void* ServerManager::BeginServer(){

	struct sockaddr_in servaddr, cliaddr;
	int listen_sock, accp_sock[THREAD_NUM];
	int addrlen = sizeof(servaddr);
	int i, status ;
	pthread_t tid[THREAD_NUM];
	pid_t pid;

	if(argc != 2) {
		printf("Use %s PortNumber\n", argv[0]);
		exit(0);
	}

	if((listen_sock = socket(PF_INET, SOCK_STREAM, 0)) < 0) {
		perror("socket Fail");
		exit(0);
	}

	memset(&servaddr, 0, sizeof(servaddr)); //0으로 초기화
	servaddr.sin_family = AF_INET;
	servaddr.sin_addr.s_addr = htonl(INADDR_ANY);
	servaddr.sin_port = htons(atoi(argv[1]));

	//bind 호출
	if(bind(listen_sock, (struct sockaddr *)&servaddr, sizeof(servaddr)) < 0) {
		perror("bind Fail");
		exit(0);
	}

	while(1) {
		listen(listen_sock, LISTENQ);

		puts("client wait....");

		accp_sock[cntNum] = accept(listen_sock, (struct sockaddr *)&cliaddr, &addrlen);
		if(accp_sock[cntNum] < 0) {
			perror("accept fail");
			exit(0);
		}

		if((status = pthread_create(&tid[cntNum], NULL, &thrfunc, (void *) &accp_sock[cntNum])) != 0) {
			printf("%d thread create error: %s\n", cntNum, strerror(status));
			exit(0);
		}

		//인자로 지정한 스레드 id가 종료하기를 기다립니다.
		pthread_join(tid[cntNum], NULL);
		cntNum++;
		if(cntNum == 5)
			cntNum = 0;
	}

	return 0;

}
