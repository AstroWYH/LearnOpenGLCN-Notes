#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aNormal;

out VS_OUT {
    vec3 normal; // wyh
} vs_out;

uniform mat4 view;
uniform mat4 model;

void main()
{
    mat3 normalMatrix = mat3(transpose(inverse(view * model))); // wyh 法线变换矩阵; 教程: 几何着色器接受的位置向量是剪裁空间坐标，所以我们应该将法向量变换到相同的空间中
    vs_out.normal = vec3(vec4(normalMatrix * aNormal, 0.0)); // wyh 先进行法线变换, 这是变换到什么空间了? 裁剪空间, 但为啥不是mvp?
    gl_Position = view * model * vec4(aPos, 1.0); 
}