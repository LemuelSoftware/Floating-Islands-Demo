shader_type spatial;
render_mode unshaded, fog_disabled, cull_disabled, shadows_disabled, specular_disabled, ambient_light_disabled;

uniform vec2 direction_1;
uniform vec2 direction_2;
uniform float alpha: hint_range(0.0, 1.0) = 0.1;
uniform sampler2D noise_texture_1;
uniform sampler2D noise_texture_2;

void fragment() {
	vec2 move_1 = direction_1 * TIME;
	vec2 move_2 = direction_2 * TIME;
	vec3 color_1 = texture(noise_texture_1, UV + move_1).rgb;
	vec3 color_2 = texture(noise_texture_2, UV + move_2).rgb;
	float a = mix(texture(noise_texture_1, UV).r, texture(noise_texture_2, UV).r, 0.5) * alpha;

	ALBEDO = mix(color_1, color_2, 0.5);
	ALPHA = a;
}
