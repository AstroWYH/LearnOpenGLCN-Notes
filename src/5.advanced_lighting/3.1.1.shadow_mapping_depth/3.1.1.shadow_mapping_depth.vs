#version 330 core
layout (location = 0) in vec3 aPos;

uniform mat4 lightSpaceMatrix;
uniform mat4 model;

void main()
{
    gl_Position = lightSpaceMatrix * model * vec4(aPos, 1.0); // wyh 世界空间变换到光源空间的矩阵
    // wyh glm::mat4 lightSpaceMatrix = lightProjection * lightView; 说明这里的光源空间对应的是光源(以光源位照相机, ×观察矩阵*透视矩阵)的裁剪空间
    // wyh 尴尬: 没搞懂这2组shader的含义, debug应该是图中的四方体, shadow应该是阴影的深度贴图, 深度纹理(不对)
    // wyh 搞懂了, 3.1.1.shadow_mapping_depth就是渲染了1个从光源视角得到的深度贴图, 然后3.1.1.debug_quad就是对这章深度贴图进行了诠释, 以灰度颜色的方式而已
}