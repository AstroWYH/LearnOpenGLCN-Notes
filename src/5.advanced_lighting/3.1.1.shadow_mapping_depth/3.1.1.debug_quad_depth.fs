#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D depthMap;
uniform float near_plane;
uniform float far_plane;

// required when using a perspective projection matrix
float LinearizeDepth(float depth)
{
    float z = depth * 2.0 - 1.0; // Back to NDC 
    return (2.0 * near_plane * far_plane) / (far_plane + near_plane - z * (far_plane - near_plane)); // wyh 采用透视矩阵的方式, 需要先将非线性深度值转变为线性
}

void main()
{             
    float depthValue = texture(depthMap, TexCoords).r; // wyh 理解: 这里是把深度写入一张纹理, 所以采用texture读取纹理获取深度值, 还是比较合理的
    // FragColor = vec4(vec3(LinearizeDepth(depthValue) / far_plane), 1.0); // perspective
    FragColor = vec4(vec3(depthValue), 1.0); // orthographic // wyh 把深度值拿来可视化, 正交矩阵的方式
}