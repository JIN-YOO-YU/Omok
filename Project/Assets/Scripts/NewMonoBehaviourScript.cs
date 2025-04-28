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
            Vector3 vector = new Vector3((float)Math.Round(transform.position.x), (float)Math.Round(transform.position.y), 0);

            Instantiate(obj, vector, Quaternion.identity);
        }  

        String str = transform.position.ToString();
        //byte[] sendBuff = Encoding.UTF8.GetBytes(str);
        byte[] byteX =  System.BitConverter.GetBytes(transform.position.x);
        byte[] byteY =  System.BitConverter.GetBytes(transform.position.y);
        byte[] sendData = new byte[byteX.Length + byteY.Length];
        Array.Copy(byteX, 0, sendData, 0, byteX.Length);
        Array.Copy(byteY, 0, sendData, byteX.Length, byteY.Length);
        socket.Send(sendData);
    }
}
