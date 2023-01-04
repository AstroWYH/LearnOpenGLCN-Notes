#version 330 core
out vec4 FragColor;

struct Material { // wyh 材质 Unity Shader最新理解, texture(material.diffuse, TexCoords), texture来采样, sampler2D就是纹理, 整个结构Material是材质
    sampler2D diffuse; // wyh 理解材质里的漫反射和高光, 变成了采样器
    sampler2D specular;
    float shininess;
}; 

struct DirLight {
    vec3 direction; // wyh 定向光的方向向量
	
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

struct PointLight {
    vec3 position; // wyh 点光源的位置向量
    
    float constant;
    float linear;
    float quadratic;
	
    vec3 ambient; // wyh 点光源的衰减系数
    vec3 diffuse;
    vec3 specular;
};

struct SpotLight {
    vec3 position; // wyh 聚光灯的位置向量
    vec3 direction; // wyh 聚光灯的spotdir, 直射方向
    float cutOff; // wyh 内切光角
    float outerCutOff; // wyh 外切光角
  
    float constant; // wyh 衰减系数
    float linear;
    float quadratic;
  
    vec3 ambient; // wyh 最后的这3项, 是灯(光源)的属性, 是几个定值
    vec3 diffuse;
    vec3 specular;       
};

#define NR_POINT_LIGHTS 4

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;

uniform vec3 viewPos;
uniform DirLight dirLight;
uniform PointLight pointLights[NR_POINT_LIGHTS];
uniform SpotLight spotLight;
uniform Material material;

// function prototypes
vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir);
vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir);
vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir); // wyh 留意3个函数需要的入参

void main() // wyh shader里的main函数
{    
    // properties
    vec3 norm = normalize(Normal);
    vec3 viewDir = normalize(viewPos - FragPos);
    
    // == =====================================================
    // Our lighting is set up in 3 phases: directional, point lights and an optional flashlight
    // For each phase, a calculate function is defined that calculates the corresponding color
    // per lamp. In the main() function we take all the calculated colors and sum them up for
    // this fragment's final color.
    // == =====================================================
    // phase 1: directional lighting
    vec3 result = CalcDirLight(dirLight, norm, viewDir);
    // phase 2: point lights
    for(int i = 0; i < NR_POINT_LIGHTS; i++)
        result += CalcPointLight(pointLights[i], norm, FragPos, viewDir);
    // phase 3: spot light
    result += CalcSpotLight(spotLight, norm, FragPos, viewDir);    
    
    FragColor = vec4(result, 1.0);
}

// calculates the color when using a directional light.
vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir)
{
    vec3 lightDir = normalize(-light.direction); // wyh 平行光方向向量为定值
    // diffuse shading
    float diff = max(dot(normal, lightDir), 0.0); // wyh 漫反射项光照方向和法线角度余弦
    // specular shading
    vec3 reflectDir = reflect(-lightDir, normal); // wyh 高光项镜面反射方向
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess); // wyh 高光项镜面反射方向和观测方向角度余弦, 并考虑余弦强度绘制
    // combine results
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords)); // wyh 环境光材质颜色与漫反射项一致, 后者取名为材质颜色?=系数K?=读取纹理的颜色?
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords)); // wyh K(材质/纹理结合的结果, 物体颜色) 和Unity Shader对应
    // wyh 复习：以这个最简单的平行光的漫反射为例，light.diffuse其实可以理解一个ratio，如果没设光照就是相当于光照是(1.0,1.0,1.0)纯白光
    // wyh 所以，就是从纹理(专门的漫反射纹理)和指定纹理坐标(遍历每个坐标)读取颜色，然后*一个ratio(漫反射光，比如0.2,0.2,0.2)，再*一个角度cos(光线,法线)
    // wyh 就得到这个片元上的颜色(仅是平行光->漫反射的颜色，其他的还要叠加)
    vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords)); // wyh Unity Shader最新说法, texture负责采样, material.diffuse是纹理
    // wyh 新理解：高光项，可以去看container2_specular.png这张高光项的纹理，就是箱子的边框有颜色(所谓金属)，中间都是黑的，而黑的部分上面公式计算得0，
    // wyh 新理解：所以高光项的计算遍历只作用于边框，其实就是在箱子的边框上叠加了一层颜色(高光项)的意思，看起来就会有金属光泽。
    return (ambient + diffuse + specular);
}

// calculates the color when using a point light.
vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(light.position - fragPos); // wyh 点光源光照方向(每个片元的)
    // diffuse shading
    float diff = max(dot(normal, lightDir), 0.0);
    // specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    // attenuation
    float distance = length(light.position - fragPos); // wyh 点光源光照距离(每个片元的)
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance)); // wyh 点光源衰减系数
    // combine results
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords)); // wyh texture(material.diffuse, TexCoords)只能是K, 实锤
    vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));
    ambient *= attenuation;
    diffuse *= attenuation;
    specular *= attenuation;
    return (ambient + diffuse + specular);
}

// calculates the color when using a spot light.
vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(light.position - fragPos); // wyh 聚光灯方向向量(每个片元的)
    // diffuse shading
    float diff = max(dot(normal, lightDir), 0.0);
    // specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    // attenuation
    float distance = length(light.position - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));    
    // spotlight intensity
    float theta = dot(lightDir, normalize(-light.direction)); // wyh 每个片元与光源的向量 和 光源直射方向(spotdir)向量 的 夹角余弦
    float epsilon = light.cutOff - light.outerCutOff; // wyh 内切角和外切角的差
    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0); // wyh 每个片元的实际光照强度(考虑聚光灯内、内外之间、外这3种)
    // combine results
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords)); // wyh 最终传出去的这3项光照, 是临时定义并计算得到的
    vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));
    ambient *= attenuation * intensity;
    diffuse *= attenuation * intensity; // wyh 所谓逐像素着色, 就是这里的程序都是针对每一个片元会做一次的, 片元不同attenuation、intensity不同
    specular *= attenuation * intensity;
    return (ambient + diffuse + specular);
}
// wyh 我像个小丑, 这里的所有FragPos都是片元的位置坐标, 因为这里是fs片元着色器, 当在vs顶点着色器中传入vertex坐标后, 应该是OPENGL自动就在光栅化过程中得到所有片元坐标了
// wyh 因此这些光照的计算, 如果放在vs里面就是逐顶点着色, 如果放在fs里面就是逐像素着色

// wyh 新的理解: texture(material.diffuse, TexCoords), texture来采样, sampler2D就是纹理, 整个结构Material是材质

// wyh 新的理解: Unity Shader里, 主要就是Shaders、Textures、Materials、Models、Scripts、Scenes
// wyh 新的理解: Models就是导入的模型, 类似我们后面要导入的模型, 或者cpp里目前简单手动输入的顶点, 再通过简单的模型矩阵转换到世界空间的摆放;
// wyh 新的理解: Materials就类似fs里面那些物体材质的属性、光源的属性等, 是通过uniform统一设置的, 在Unity里就直接由3DMAX等做好了导入, 就带有了这些属性, 当然fs里后续也会这样导入;
// wyh 新的理解: Textures就是纹理, 一张张图片; Scripts就是些额外的程序脚本; Scenes就是整个场景;
// wyh 新的理解: Shaders就是vs、fs里写的代码了;