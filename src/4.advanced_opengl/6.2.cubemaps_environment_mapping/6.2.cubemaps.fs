#version 330 core
out vec4 FragColor;

in vec3 Normal;
in vec3 Position;

uniform vec3 cameraPos; // wyh 摄像机位置
uniform samplerCube skybox; // wyh 箱子也换成了立方体贴图的采样器?

void main() // wyh 比6.1多的核心: 给箱子添加(环境映射)天空盒反射的属性, 去除箱子的纹理
{    
    vec3 I = normalize(Position - cameraPos); // wyh 视角方向向量
    vec3 R = reflect(I, normalize(Normal)); // wyh 反射方向向量
    FragColor = vec4(texture(skybox, R).rgb, 1.0); // wyh 立方体的颜色; 教程: 最终的R向量将会作为索引/采样立方体贴图的方向向量
}