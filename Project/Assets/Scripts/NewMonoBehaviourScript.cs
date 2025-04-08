using Unity.Profiling;
using UnityEditor;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using JetBrains.Annotations;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Animator animator;
    private float nextX = 0;
    private float nextY = 0;
    public GameObject obj;
    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("172.16.17.69"), 8211);
        //소켓 생성
    Socket socket;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        Instantiate(obj, new Vector2(1,1), Quaternion.identity);
        //끝점 생성 (서버 IP 및 포트)
        socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        //소켓을 끝점에 연결
        socket.Connect(endPoint);
        Console.WriteLine("Connect...");
    }

    void Update()
    {
        nextX = Input.GetAxis("Horizontal");
        nextY = Input.GetAxis("Vertical");
 
        if(nextX == 0f && nextY == 0f){
            animator.SetInteger("AnimationState", 0);
        }
        else{
            animator.SetInteger("AnimationState", 1);
            transform.Translate(new Vector2(nextX, nextY).normalized * Time.deltaTime * moveSpeed);
        }

        if(Input.GetKeyDown(KeyCode.E)){
            Instantiate(obj, transform.position, Quaternion.identity);
            
        }  

        String str = transform.position.ToString();
        byte[] sendBuff = Encoding.UTF8.GetBytes(str);
        socket.Send(sendBuff);
        byte[] recvBuff = new byte[1024];
        int recvBytes = socket.Receive(recvBuff);
        string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
        Debug.Log(recvData);
    }
}
