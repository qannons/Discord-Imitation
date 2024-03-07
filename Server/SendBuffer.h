#pragma once
//
//class SendBufferChunk;
//
///*----------------
//	SendBuffer
//-----------------*/
//
//class SendBuffer
//{
//public:
//	SendBuffer(SendBufferChunkRef owner, BYTE* buffer, UINT32 allocSize);
//	~SendBuffer();
//
//	BYTE* Buffer() { return _buffer; }
//	UINT32		AllocSize() { return _allocSize; }
//	UINT32		WriteSize() { return _writeSize; }
//	void		Close(UINT32 writeSize);
//
//private:
//	BYTE* _buffer;
//	UINT32				_allocSize = 0;
//	UINT32				_writeSize = 0;
//	SendBufferChunkRef	_owner;
//};
//
///*--------------------
//	SendBufferChunk
//--------------------*/
//
//class SendBufferChunk : public enable_shared_from_this<SendBufferChunk>
//{
//	enum
//	{
//		SEND_BUFFER_CHUNK_SIZE = 6000
//	};
//
//public:
//	SendBufferChunk();
//	~SendBufferChunk();
//
//	void				Reset();
//	SendBufferRef		Open(UINT32 allocSize);
//	void				Close(UINT32 writeSize);
//
//	bool				IsOpen() { return _open; }
//	BYTE* Buffer() { return &_buffer[_usedSize]; }
//	UINT32				FreeSize() { return static_cast<UINT32>(_buffer.size()) - _usedSize; }
//
//private:
//	array<BYTE, SEND_BUFFER_CHUNK_SIZE>		_buffer = {};
//	bool									_open = false;
//	UINT32									_usedSize = 0;
//};
//
///*---------------------
//	SendBufferManager
//----------------------*/
//
//class SendBufferManager
//{
//public:
//	SendBufferRef		Open(UINT32 size);
//
//private:
//	SendBufferChunkRef	Pop();
//	void				Push(SendBufferChunkRef buffer);
//
//	static void			PushGlobal(SendBufferChunk* buffer);
//
//private:
//	mutex _mutex;
//	vector<SendBufferChunkRef> _sendBufferChunks;
//};
//extern SendBufferManager GSendBufferManager;